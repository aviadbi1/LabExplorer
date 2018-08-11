using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoadScholar.Models;
using RoadScholar.BL;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RoadScholar.Controllers
{
    public class StudentController : BaseController
    {
        RoadScholarContext rsContext = new RoadScholarContext();

        // GET: Student
        public ActionResult Index(StudentLoginViewModel model)
        {
            if (Validate(model))
                return RedirectToAction("ViewRoomOrWaitingScreen", "Student", model);
            TempData["alertMessage"] = Resources.Resources.LoginInvalid;
            return RedirectToAction("StudentLogin", "Account");
        }

        private bool Validate(StudentLoginViewModel model)
        {
            StudentData sData = rsContext.getStudent(model.PhoneNumber, model.Room);
            if (rsContext.getRoom(model.Room) != null && sData != null && sData.RoomId == model.Room)
                return true;
            return false;
        }

        // GET: Student/ViewRoomOrWaitingScreenRefresh
        public ActionResult ViewRoomOrWaitingScreenRefresh(string phone, long roomID)
        {
            return RedirectToAction("ViewRoomOrWaitingScreen", new StudentLoginViewModel(roomID, phone));
        }

        // GET: Student/ViewRoomOrWaitingScreen
        public ActionResult ViewRoomOrWaitingScreen(StudentLoginViewModel model)
        {
            RoomData roomData = rsContext.getRoom(model.Room);
            ActivityData activityData = rsContext.getActivity(roomData.CurrentActivityId);
            if (activityData == null)
            {
                //Waiting Screen
                return RedirectToAction("Wait", model);
            }
            else
            {
                if (activityData is TrueFalseQuestionData)
                {
                    //True/False Question
                    TrueFalseQuestion tfq = (TrueFalseQuestion)Adapting.getTrueFalseQuestionFromData((TrueFalseQuestionData)activityData);
                    return RedirectToAction("TrueFalseQuestion", new { expId = -1, currActivityIndex = -1, activityId = tfq.id, numOfActivities = 0, studentPhone = model.PhoneNumber, studentRoom = model.Room });
                }
                else if (activityData is ShortAnswerQuestionData)
                {
                    //Short Answer Question
                    ShortAnswerQuestion saq = (ShortAnswerQuestion)Adapting.getShortAnswerQuestionFromData((ShortAnswerQuestionData)activityData);
                    return RedirectToAction("ShortAnswerQuestion", new { expId = -1, currActivityIndex = -1, activityId = saq.id, numOfActivities = 0, studentPhone = model.PhoneNumber, studentRoom = model.Room });
                }
                else if (activityData is AmericanQuestionData)
                {
                    //American Answer Question
                    AmericanQuestion aq = (AmericanQuestion)Adapting.getAmericanQuestionFromData((AmericanQuestionData)activityData);
                    return RedirectToAction("AmericanQuestion", new { expId = -1, currActivityIndex = -1, activityId = aq.id, numOfActivities = 0, studentPhone = model.PhoneNumber, studentRoom = model.Room });
                }

                else if (activityData is ExperimentData)
                {
                    // Experiment
                    Experiment exp = (Experiment)Adapting.getExperimentFromData((ExperimentData)activityData);
                    if (exp.ActiveExpID == 0)
                    {
                        //Waiting Screen
                        Thread.Sleep(2000);
                        return RedirectToAction("Wait", model);
                    }
                    return RedirectToAction("GroupCreation", new { expId = exp.id, studentPhone = model.PhoneNumber, studentRoom = model.Room, currActivityIndex = 0 });
                }
            }

            return View();
        }

        // GET: Student/GetRoomState
        public ActionResult GetRoomState(string phone, long roomID)
        {
            RoomData roomData = rsContext.getRoom(roomID);
            StudentData studentData = rsContext.getStudent(phone, roomID);
            roomData.addStudent(studentData);
            rsContext.SaveChanges();

            var info = new List<object>();

            info.Add(new
            {
                CurrentActivityID = roomData.CurrentActivityId
            });

            return Json(info, JsonRequestBehavior.AllowGet);
        }

        // GET: Student/Wait
        public ActionResult Wait(StudentLoginViewModel studentLogin)
        {
            return View(studentLogin);
        }

        // GET: Student/GroupCreation
        public ActionResult GroupCreation(long expId, string studentPhone, long studentRoom)
        {
            ExperimentData expData = (ExperimentData)rsContext.getActivity(expId);
            ActiveExperimentData aeData = rsContext.getActiveExperiment(expData.ActiveExpID);
            TempData["ActiveExpID"] = aeData.ActiveExpID;
            TempData["StudentPhone"] = studentPhone;
            TempData["StudentRoom"] = studentRoom;
            return View();
        }

        // GET: Student/CreateGroupForm
        [MethodImpl(MethodImplOptions.Synchronized)]
        public ActionResult CreateGroupForm(long ActiveExpId, string studentPhone, long studentRoom)
        {
            StudentData sd = rsContext.getStudent(studentPhone, studentRoom);
            if (!sd.isAvailable())
            {
                TempData["alertMessage"] = Resources.Resources.YouAreAlreadyInAGroup;
                return RedirectToAction("StudentLogin", "Account");
            }
            ActiveExperimentData aeData = rsContext.getActiveExperiment(ActiveExpId);
            if (aeData.NumberCreatedGroups == aeData.NumberOfGroups)
            {
                return RedirectToAction("ShowNoAvailableGroups");
            }
            else
            {
                TempData["MaxStudentsPerGroup"] = aeData.MaxStudentPerGroup;
                TempData["StudentPhone"] = studentPhone;
                TempData["StudentRoom"] = studentRoom;
                TempData["ActiveExpID"] = ActiveExpId;
                return View();
            }

        }


        // GET: Student/ShowNoAvailableGroups
        public ActionResult ShowNoAvailableGroups()
        {
            return View();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public ActionResult SaveNewGroup(List<string> allStudentPhones, long activeExpId, long roomId)
        {
            // add group to activeExp
            List<StudentData> students = new List<StudentData>();
            bool allAvailable = true;
            StudentData sd;
            string errMsg = "";

            foreach (string currentStudentPhone in allStudentPhones)
            {
                sd = rsContext.getStudent(currentStudentPhone, roomId);
                if (sd == null || !sd.isAvailable())
                {
                    allAvailable = false;
                    if (sd == null)
                    {
                        errMsg = Resources.Resources.StudentErrorMsg1;
                    }
                    else
                    {
                        errMsg = Resources.Resources.StudentErrorMsg2;
                    }
                }
                students.Add(sd);
            }

            if (!allAvailable)
            {
                TempData["StudentPhone"] = allStudentPhones.ElementAt(0);
                TempData["ActiveExpID"] = activeExpId;
                TempData["StudentRoom"] = roomId;
                TempData["errorMessage"] = errMsg;
                return PartialView("CreateGroupForm");
            }

            GroupData groupData = new GroupData(activeExpId, students);
            rsContext.addGroup(groupData);

            foreach (StudentData currentSD in students)
            {
                currentSD.GroupID = groupData.GroupID;
            }

            string studentPhone = allStudentPhones[0];
            ActiveExperimentData aeData = rsContext.getActiveExperiment(activeExpId);
            aeData.IncrementCreatedGroupsCounter();
            rsContext.SaveChanges();
            return RedirectToAction("Experiment", new { expId = aeData.ExpID, studentPhone = studentPhone, studentRoom = roomId, currActivityIndex = 0 });
        }

        // GET: Student/Experiment
        public ActionResult Experiment(long expId, string studentPhone, long studentRoom, int currActivityIndex)
        {
            // Update group Progress
            StudentData student = rsContext.getStudent(studentPhone, studentRoom);
            GroupData group = rsContext.getGroupById(student.GroupID);
            if (group == null)
                return RedirectToAction("Wait", new StudentLoginViewModel(studentRoom, studentPhone));
            group.Progress++;
            rsContext.SaveChanges();

            ExperimentData expData = (ExperimentData)rsContext.getActivity(expId);
            if (currActivityIndex < expData.activities.Count)
            {
                ActivityData currActivityData = expData.activities.Single(s => s.experimentOrder == currActivityIndex);
                return RedirectToAction(currActivityData.Type, (new { success = true, expId = expId, currActivityIndex = currActivityIndex, numOfActivities = expData.activities.Count, activityId = currActivityData.id, studentPhone = studentPhone, studentRoom = studentRoom }));
            }
            else
            {
                return RedirectToAction("DisplaySubmittedExperiment", new { flag = expData.activities.ElementAt(currActivityIndex - 1).Type == "Measurement" });
            }
        }

        public ActionResult DisplaySubmittedExperiment(bool flag)
        {
            if (flag)
            {
                TempData["alertSuccessMessage"] = Resources.Resources.AnswerSubmitted;
                return RedirectToAction("StudentLogin", "Account", new { flag = true });
            }
            TempData["alertSuccessMessage"] = Resources.Resources.AnswerSubmitted;
            return RedirectToAction("StudentLogin", "Account");
        }

        // GET: Student/Measurement
        public ActionResult Measurement(long activityId, string studentPhone, long studentRoom, long expId, int currActivityIndex, int numOfActivities)
        {
            MeasurementData measurementData = (MeasurementData)rsContext.getActivity(activityId);
            TempData["NumOfMeasures"] = measurementData.NumOfMeasures;
            TempData["DifferenceBetweenMeasures"] = measurementData.DifferenceBetweenMeasures;
            TempData["NumOfParametersToMeasure"] = measurementData.NumOfParametersToMeasure;
            TempData["WindowOpenTimeSeconds"] = measurementData.WindowOpenTimeSeconds;
            StudentData studentData = rsContext.getStudent(studentPhone, studentRoom);
            TempData["GroupID"] = studentData.GroupID;
            TempData["RoomID"] = studentData.RoomId;
            TempData["ExpID"] = expId;
            TempData["CurrActivityIndex"] = currActivityIndex;
            TempData["StudentPhone"] = studentPhone;
            TempData["currActivityIndex"] = currActivityIndex;
            TempData["numOfActivities"] = numOfActivities;

            // TODO pass the instructions to view
            int i = 0;
            foreach (MeasureInstructionData mi in measurementData.measurementInstructions)
            {
                TempData["Instruction" + i] = mi.MeasurementInstruction;
                i++;
            }

            rsContext.SaveChanges();
            if (currActivityIndex == 0) // first in exp activities
                return PartialView();
            return View();
        }

        // POST: Student/SaveMeasurementsInDB
        [HttpPost]
        public ActionResult SaveMeasurementsInDB(long GroupID, long RoomId, long ExpID, string StudentPhone, long CurrActivityIndex, List<List<double>> allMeasurements)
        {

            MeasurementByGroupIdData mbgid = rsContext.GetMeasurementByGroupID(ExpID, RoomId, GroupID, CurrActivityIndex);
            if (mbgid == null)
            {
                mbgid = new MeasurementByGroupIdData(ExpID, RoomId, GroupID, CurrActivityIndex);

                rsContext.AddMeasurementByGroupID(mbgid);
                rsContext.SaveChanges();
            }

            GroupData group = rsContext.getGroupById(GroupID);

            foreach (List<double> currentOneMeausre in allMeasurements)
            {
                OneMeasureByGroupIdData oneM = new OneMeasureByGroupIdData(mbgid.id);
                foreach (double currentMeasure in currentOneMeausre)
                {
                    oneM.addParameterMeasurement(currentMeasure);
                }
                rsContext.AddOneMeasureByGroupID(oneM);
                mbgid.addOneMeasure(oneM);
            }
            rsContext.SaveChanges();

            group.AddMeasurement(mbgid);

            return RedirectToAction("Experiment", new { expId = ExpID, studentPhone = StudentPhone, studentRoom = RoomId, currActivityIndex = CurrActivityIndex + 1 });

        }


        // GET: Student/TrueFalseQuestion
        public ActionResult TrueFalseQuestion(long activityId, string studentPhone, long studentRoom, long expId, int currActivityIndex, int numOfActivities)
        {
            TrueFalseQuestionData tfqData = (TrueFalseQuestionData)rsContext.getActivity(activityId);

            TempData["Question"] = tfqData.question;
            TempData["studentPhone"] = studentPhone;
            TempData["studentRoom"] = studentRoom;
            TempData["activityId"] = activityId;
            TempData["expId"] = expId;
            TempData["currActivityIndex"] = currActivityIndex;
            TempData["numOfActivities"] = numOfActivities;

            if (currActivityIndex == 0) // first in exp activities
                return PartialView();
            return View();
        }

        // POST: Student/DisplaySubmitedTfQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DisplaySubmitedTfQuestion(TrueFalseQuestionResponse tfqr)
        {
            TrueFalseQuestionData tfqData = (TrueFalseQuestionData)rsContext.getActivity(tfqr.ActivityId);

            if (tfqr.Answer)
            {
                tfqData.updateCounters(tfqr.PhoneNumber, "Yes");
                tfqData.AddAnswerToDictionary(tfqData.id, tfqr.PhoneNumber, "Yes", tfqr.RoomId);
            }
            else
            {
                tfqData.updateCounters(tfqr.PhoneNumber, "No");
                tfqData.AddAnswerToDictionary(tfqData.id, tfqr.PhoneNumber, "No", tfqr.RoomId);
            }

            rsContext.SaveChanges();

            if (tfqr.ExpId == -1)
            {
                TempData["alertSuccessMessage"] = Resources.Resources.AnswerSubmitted;
                return RedirectToAction("StudentLogin", "Account");
            }
            else
            {
                return RedirectToAction("Experiment", new { expId = tfqr.ExpId, studentPhone = tfqr.PhoneNumber, studentRoom = tfqr.RoomId, currActivityIndex = tfqr.CurrActivityIndex + 1 });
            }
        }


        // GET: Student/AmericanQuestion
        public ActionResult AmericanQuestion(long activityId, string studentPhone, long studentRoom, long expId, int currActivityIndex, int numOfActivities)
        {
            AmericanQuestionData aqData = (AmericanQuestionData)rsContext.getActivity(activityId);

            TempData["Question"] = aqData.question;
            TempData["studentPhone"] = studentPhone;
            TempData["studentRoom"] = studentRoom;
            TempData["activityId"] = activityId;
            TempData["FirstAnswer"] = aqData.firstAnswer;
            TempData["SecondAnswer"] = aqData.secondAnswer;
            TempData["ThirdAnswer"] = aqData.thirdAnswer;
            TempData["FourthAnswer"] = aqData.fourthAnswer;
            TempData["expId"] = expId;
            TempData["currActivityIndex"] = currActivityIndex;
            TempData["numOfActivities"] = numOfActivities;

            if (currActivityIndex == 0) // first in exp activities
                return PartialView();
            return View();
        }

        // GET: Student/ShortAnswerQuestion
        public ActionResult ShortAnswerQuestion(long activityId, string studentPhone, long studentRoom, long expId, int currActivityIndex, int numOfActivities)
        {
            ShortAnswerQuestionData saqData = (ShortAnswerQuestionData)rsContext.getActivity(activityId);

            TempData["Question"] = saqData.question;
            TempData["studentPhone"] = studentPhone;
            TempData["studentRoom"] = studentRoom;
            TempData["activityId"] = activityId;
            TempData["correctAnswerString"] = saqData.correctAnswerString;
            TempData["expId"] = expId;
            TempData["currActivityIndex"] = currActivityIndex;
            TempData["numOfActivities"] = numOfActivities;

            if (currActivityIndex == 0) // first in exp activities
                return PartialView();
            return View();
        }

        // POST: Student/DisplaySubmitedSAQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DisplaySubmitedSAQuestion(ShortAnswerQuestionResponse saqr)
        {
            ShortAnswerQuestionData saqData = (ShortAnswerQuestionData)rsContext.getActivity(saqr.ActivityId);
            saqData.AddAnswerToDictionary(saqData.id, saqr.PhoneNumber, saqr.Answer, saqr.RoomId);

            rsContext.SaveChanges();

            if (saqr.ExpId == -1)
            {
                TempData["alertSuccessMessage"] = Resources.Resources.AnswerSubmitted;
                return RedirectToAction("StudentLogin", "Account");
            }
            else
            {
                return RedirectToAction("Experiment", new { expId = saqr.ExpId, studentPhone = saqr.PhoneNumber, studentRoom = saqr.RoomId, currActivityIndex = saqr.CurrActivityIndex + 1 });
            }
        }


        // POST: Student/DisplaySubmitedAmericanQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DisplaySubmitedAmericanQuestion(AmericanQuestionResponse aqr)
        {
            AmericanQuestionData aqData = (AmericanQuestionData)rsContext.getActivity(aqr.ActivityId);

            if (aqr.Answer == 1)
            {
                aqData.updateCounters(aqr.PhoneNumber, 1);
                aqData.AddAnswerToDictionary(aqData.id, aqr.PhoneNumber, "Answer 1", aqr.RoomId);
            }
            else if (aqr.Answer == 2)
            {
                aqData.updateCounters(aqr.PhoneNumber, 2);
                aqData.AddAnswerToDictionary(aqData.id, aqr.PhoneNumber, "Answer 2", aqr.RoomId);
            }
            else if (aqr.Answer == 3)
            {
                aqData.updateCounters(aqr.PhoneNumber, 3);
                aqData.AddAnswerToDictionary(aqData.id, aqr.PhoneNumber, "Answer 3", aqr.RoomId);
            }
            else if (aqr.Answer == 4)
            {
                aqData.updateCounters(aqr.PhoneNumber, 4);
                aqData.AddAnswerToDictionary(aqData.id, aqr.PhoneNumber, "Answer 4", aqr.RoomId);
            }

            rsContext.SaveChanges();

            if (aqr.ExpId == -1)
            {
                TempData["alertSuccessMessage"] = Resources.Resources.AnswerSubmitted;
                return RedirectToAction("StudentLogin", "Account");
            }
            else
            {
                return RedirectToAction("Experiment", new { expId = aqr.ExpId, studentPhone = aqr.PhoneNumber, studentRoom = aqr.RoomId, currActivityIndex = aqr.CurrActivityIndex + 1 });
            }
        }

        // GET: Student/Instruction
        public ActionResult Instruction(long activityId, string studentPhone, long studentRoom, long expId, int currActivityIndex, int numOfActivities)
        {
            InstructionData instructionData = (InstructionData)rsContext.getActivity(activityId);
            TempData["Command"] = instructionData.Command;
            TempData["studentPhone"] = studentPhone;
            TempData["studentRoom"] = studentRoom;
            TempData["activityId"] = activityId;
            TempData["expId"] = expId;
            TempData["currActivityIndex"] = currActivityIndex;
            TempData["numOfActivities"] = numOfActivities;

            rsContext.SaveChanges();

            if (currActivityIndex == 0) // first in exp activities
                return PartialView();
            return View();
        }

        // GET: Student/Image
        public ActionResult Image(long activityId, string studentPhone, long studentRoom, long expId, int currActivityIndex, int numOfActivities)
        {
            ImageData imageData = (ImageData)rsContext.getActivity(activityId);
            TempData["Title"] = imageData.Title;
            TempData["URL"] = imageData.URL;
            TempData["studentPhone"] = studentPhone;
            TempData["studentRoom"] = studentRoom;
            TempData["activityId"] = activityId;
            TempData["expId"] = expId;
            TempData["currActivityIndex"] = currActivityIndex;
            TempData["numOfActivities"] = numOfActivities;

            rsContext.SaveChanges();

            if (currActivityIndex == 0) // first in exp activities
                return PartialView();
            return View();
        }

        // GET: Student/Video
        public ActionResult Video(long activityId, string studentPhone, long studentRoom, long expId, int currActivityIndex, int numOfActivities)
        {
            VideoData videoData = (VideoData)rsContext.getActivity(activityId);
            TempData["URL"] = videoData.URL;
            TempData["studentPhone"] = studentPhone;
            TempData["studentRoom"] = studentRoom;
            TempData["activityId"] = activityId;
            TempData["expId"] = expId;
            TempData["currActivityIndex"] = currActivityIndex;
            TempData["numOfActivities"] = numOfActivities;

            rsContext.SaveChanges();

            if (currActivityIndex == 0) // first in exp activities
                return PartialView();
            return View();
        }

        // POST: Student/DisplaySubmitedInstruction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DisplaySubmitedInstruction(InstructionResponse ir)
        {
            return RedirectToAction("Experiment", new { expId = ir.ExpId, studentPhone = ir.PhoneNumber, studentRoom = ir.RoomId, currActivityIndex = ir.CurrActivityIndex + 1 });
        }

        // POST: Student/DisplaySubmitedImage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DisplaySubmitedImage(ImageResponse ir)
        {
            return RedirectToAction("Experiment", new { expId = ir.ExpId, studentPhone = ir.PhoneNumber, studentRoom = ir.RoomId, currActivityIndex = ir.CurrActivityIndex + 1 });
        }

        // POST: Student/DisplaySubmitedVideo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DisplaySubmitedVideo(VideoResponse vr)
        {
            return RedirectToAction("Experiment", new { expId = vr.ExpId, studentPhone = vr.PhoneNumber, studentRoom = vr.RoomId, currActivityIndex = vr.CurrActivityIndex + 1 });
        }
    }
}