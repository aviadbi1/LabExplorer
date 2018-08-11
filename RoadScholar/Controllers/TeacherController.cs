
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RoadScholar.Models;
using RoadScholar.BL;
using System.Web;
using System.IO;
using System.Data;
using System.Text;

namespace RoadScholar.Controllers
{
    [Authorize]
    public class TeacherController : BaseController
    {
        RoadScholarContext rsContext = new RoadScholarContext();

        public ActionResult ShowCurrentActivity()
        {
            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            RoomData roomData = rsContext.getRoom(teacherData.RoomId);
            ActivityData currActivity = rsContext.getActivity(roomData.CurrentActivityId);
            if (currActivity == null)
            {
                TempData["alertMessage"] = Resources.Resources.ThereIsNoActivityActivated;
                return RedirectToAction("Dashboard");
            }
            else
            {
                if (currActivity is TrueFalseQuestionData)
                {
                    return View("DisplayTrueFalseQuestion", Adapting.getTrueFalseQuestionFromData((TrueFalseQuestionData)currActivity));
                }
                else if (currActivity is ShortAnswerQuestionData)
                {
                    return View("DisplayShortAnswerQuestion", Adapting.getShortAnswerQuestionFromData((ShortAnswerQuestionData)currActivity));
                }
                else if (currActivity is AmericanQuestionData)
                {
                    return View("DisplayAmericanQuestion", Adapting.getAmericanQuestionFromData((AmericanQuestionData)currActivity));
                }
                else // experiment
                {
                    ExperimentData expData = (ExperimentData)currActivity;
                    if (expData.ActiveExpID == 0)
                    {
                        return RedirectToAction("GroupSelection", new { ExpID = expData.id });
                    }
                    else
                    {
                        ActiveExperimentData ae = rsContext.getActiveExperiment(expData.ActiveExpID);
                        return StudentsProgress(Adapting.getActiveExperimentFromData(ae), true);
                    }
                }
            }
        }

        public ActionResult RegisterStudents(List<Student> allOfStudents)
        {
            if (allOfStudents == null)
            {
                TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
                TempData["RoomID"] = teacherData.RoomId;

                return View();
            }
            else {
                TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
                foreach (Student s in allOfStudents)
                {
                    s.RoomId = teacherData.RoomId;
                }
                List<StudentData> studentsData = Adapting.getStudentListAsData(allOfStudents);
                string stud = teacherData.registerStudentsFromList(studentsData);
                if (!stud.Equals(""))
                    TempData["errorMessage"] = Resources.Resources.ThoseStudentsAreAlreadyRegistered + stud + Resources.Resources.AllTheOtherStudentsWereRegistered;
                else
                    TempData["infoMessage"] = Resources.Resources.StudentsRegisteredSuccessfully;
                return PartialView();
            }
        }

        private bool checkData(List<Student> allOfStudents)
        {
            bool ans = false;
            foreach (Student s in allOfStudents)
            {
                if (s.FirstName == null ||
                   s.LastName == null ||
                   s.PhoneNumber == null ||
                   s.Email == null) // TODO check Email
                {
                    ans = true;
                }
            }

            return ans;
        }

        // GET: Teacher/Dashboard
        public ActionResult Dashboard()
        {
            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            Teacher teacher = Adapting.getTeacherFromData(teacherData);
            return View(teacher);
        }

        // GET: Teacher/DashboardAfterLog
        public ActionResult DashboardAfterLog(long id)
        {
            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            RoomData roomData = rsContext.getRoom(teacherData.RoomId);
            roomData.updateCurrentActivityId(0);
            rsContext.SaveChanges();

            // Save activity log
            ActivityData actData = rsContext.getActivity(id);
            if (actData is TrueFalseQuestionData)
            {
                TFQLogData tfqlData = new TFQLogData((TrueFalseQuestionData)actData, DateTime.Now);
                rsContext.addActivityLog(tfqlData);
                ((TrueFalseQuestionData)actData).reset();
                rsContext.SaveChanges();
            }
            else if (actData is AmericanQuestionData)
            {
                AmericanLogData aqlData = new AmericanLogData((AmericanQuestionData)actData, DateTime.Now);
                rsContext.addActivityLog(aqlData);
                ((AmericanQuestionData)actData).reset();
                rsContext.SaveChanges();
            }
            else if (actData is ShortAnswerQuestionData)
            {
                SALogData saqlData = new SALogData((ShortAnswerQuestionData)actData, DateTime.Now);
                rsContext.addActivityLog(saqlData);
                ((ShortAnswerQuestionData)actData).reset();
                rsContext.SaveChanges();
            }
            else
            {
                ActiveExperimentData aed = rsContext.getActiveExperiment(id);
                actData = rsContext.getActivity(aed.ExpID); // The experiment
                // Save Experiment Log
                long NumOfMeasuresToCollaborativeGraph = ((ExperimentData)actData).getNumOfMeasures();
                long DiffBetweenMeasuresToCollaborativeGraph = ((ExperimentData)actData).getDiffBetweenMeasures();
                ExperimentLogData expLogData = new ExperimentLogData((ExperimentData)actData, DateTime.Now, NumOfMeasuresToCollaborativeGraph, DiffBetweenMeasuresToCollaborativeGraph);
                rsContext.addActivityLog(expLogData);

                // Save for each group its students
                foreach (GroupData gData in rsContext.getGroupsByActiveExpID(id))
                {
                    gData.SaveAllStudentInGroupAsList();
                }

                // Initialize Students and ExperimentData
                ActiveExperimentData activeExpData = rsContext.getActiveExperiment(id);
                List<StudentData> studentsInRoom = rsContext.getStudentsByTeacher(roomData.id);
                foreach (StudentData studentData in studentsInRoom)
                {
                    studentData.GroupID = 0;
                }
                ExperimentData expData = (ExperimentData)rsContext.getActivity(activeExpData.ExpID);
                expData.ActiveExpID = 0;
                rsContext.SaveChanges();
            }

            return RedirectToDashboard();
        }

        // GET: Teacher/CreateActivity
        public ActionResult CreateActivity()
        {
            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            if (teacherData == null)
                throw new ArgumentNullException(nameof(teacherData));
            Teacher teacher = Adapting.getTeacherFromData(teacherData);
            return View(teacher);
        }


        // GET: Teacher/CreateExperiment
        public ActionResult CreateExperiment()
        {
            return View();
        }

        // POST: Teacher/EditExperiment
        [HttpPost]
        public ActionResult EditExperiment(Experiment exp)
        {
            ActivityData tempExp = rsContext.getActivity(exp.ActivityName);

            if (tempExp == null)
            {
                ExperimentData expData = Adapting.getExperimentAsData(exp);
                rsContext.addActivity(expData);
                exp.activities = new List<Activity>();
                exp = Adapting.getExperimentFromData(expData);
            }
            else
            {
                if (tempExp is ExperimentData)
                {
                    ExperimentData expData = (ExperimentData)tempExp;
                    exp = Adapting.getExperimentFromData(expData);
                }
                else
                {
                    throw new Exception("you try to create an activity with the same name of an alredy existing activity");
                }
            }

            return View(exp);
        }

        // GET: Teacher/EditExperiment
        public ActionResult EditExperiment(long ExpID)
        {
            ExperimentData expData = (ExperimentData)rsContext.getActivity(ExpID);
            Experiment exp = Adapting.getExperimentFromData(expData);
            return View(exp);
        }

        // GET: Teacher/CreateTrueFalseQuestion
        public ActionResult CreateTrueFalseQuestion()
        {
            TempData["ExpID"] = -1;
            return View();
        }

        // GET: Teacher/CreateTrueFalseQuestionExp
        public ActionResult CreateTrueFalseQuestionExp(long ExpID)
        {
            TempData["ExpID"] = ExpID;
            return View("CreateTrueFalseQuestion");
        }

        // GET: Teacher/CreateAmericanQuestion
        public ActionResult CreateAmericanQuestion()
        {
            TempData["ExpID"] = -1;
            return View();
        }

        // GET: Teacher/CreateAmericanQuestionExp
        public ActionResult CreateAmericanQuestionExp(long ExpID)
        {
            TempData["ExpID"] = ExpID;
            return View("CreateAmericanQuestion");
        }

        // GET: Teacher/CreateShortAnswerQuestion
        public ActionResult CreateShortAnswerQuestion()
        {
            TempData["ExpID"] = -1;
            return View();
        }

        // GET: Teacher/CreateShortAnswerQuestionExp
        public ActionResult CreateShortAnswerQuestionExp(long ExpID)
        {
            TempData["ExpID"] = ExpID;
            return View("CreateShortAnswerQuestion");
        }


        // POST: Teacher/SaveInstruction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveInstruction(Instruction instruction)
        {

            ExperimentData experimentData = (ExperimentData)rsContext.getActivity(instruction.expID);
            InstructionData instructionData = Adapting.getInstructionAsData(instruction);
            instructionData.experimentOrder = experimentData.activities.Count;
            experimentData.addStep(instructionData);
            rsContext.SaveChanges();
            return RedirectToAction("EditExperiment", new { ExpID = instruction.expID });
        }

        // POST: Teacher/SaveImage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveImage(Image image)
        {

            ExperimentData experimentData = (ExperimentData)rsContext.getActivity(image.expID);
            ImageData imageData = Adapting.getImageAsData(image);
            imageData.experimentOrder = experimentData.activities.Count;
            experimentData.addStep(imageData);
            rsContext.SaveChanges();
            return RedirectToAction("EditExperiment", new { ExpID = image.expID });
        }

        // POST: Teacher/SaveVideo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveVideo(Video video)
        {

            ExperimentData experimentData = (ExperimentData)rsContext.getActivity(video.expID);
            VideoData videoData = Adapting.getVideoAsData(video);
            videoData.experimentOrder = experimentData.activities.Count;
            experimentData.addStep(videoData);
            rsContext.SaveChanges();
            return RedirectToAction("EditExperiment", new { ExpID = video.expID });
        }

        // POST: Teacher/EditMeasurement
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMeasurement(Measurement measurement)
        {
            ExperimentData experimentData = (ExperimentData)rsContext.getActivity(measurement.expID);
            MeasurementData measurementData = Adapting.getMeasurementAsData(measurement);
            measurementData.RoomId = 0;
            measurementData.experimentOrder = experimentData.activities.Count;
            experimentData.addStep(measurementData);
            rsContext.SaveChanges();
            return RedirectToAction("AddInstructionsForMeasures", new { MeasurementID = measurementData.id, NumOfParametersToMeasure = measurementData.NumOfParametersToMeasure });
        }

        public ActionResult AddInstructionsForMeasures(long MeasurementID, long NumOfParametersToMeasure)
        {
            TempData["MeasurementID"] = MeasurementID;
            TempData["NumOfParametersToMeasure"] = NumOfParametersToMeasure;
            return View();
        }

        public ActionResult SaveInstructionsForMeasurementsInDB(List<string> allInstructions, long MeasurementID)
        {
            MeasurementData measurementData = rsContext.getMeasurementByID(MeasurementID);
            foreach (string instruction in allInstructions)
            {
                measurementData.AddInstruction(instruction);
            }

            rsContext.SaveChanges();
            return RedirectToAction("EditExperiment", new { ExpID = measurementData.expID });
        }

        // GETPOST: Teacher/SaveExperiment
        public ActionResult SaveExperiment(long ExpId)
        {
            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            ExperimentData experimentData = (ExperimentData)rsContext.getActivity(ExpId);
            rsContext.SaveChanges();
            return RedirectToDashboard();
        }

        // POST: Teacher/SaveTrueFalseQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTrueFalseQuestion(TrueFalseQuestion ques)
        {

            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);

            if (ques.expID == -1)
            {
                ques.RoomId = teacherData.RoomId;
                TrueFalseQuestionData tfqData = Adapting.getTrueFalseQuestionAsData(ques);
                rsContext.addActivity(tfqData);
                rsContext.SaveChanges();
                return RedirectToDashboard();
            }
            else
            {
                ques.RoomId = 0;
                TrueFalseQuestionData tfqData = Adapting.getTrueFalseQuestionAsData(ques);
                rsContext.addActivity(tfqData);
                ExperimentData experimentData = (ExperimentData)rsContext.getActivity(ques.expID);
                tfqData.experimentOrder = experimentData.activities.Count;
                experimentData.addStep(tfqData);
                rsContext.SaveChanges();
                return RedirectToAction("EditExperiment", new { ExpID = ques.expID });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveShortAnswerQuestion(ShortAnswerQuestion ques)
        {
            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);

            if (ques.expID == -1)
            {
                ques.RoomId = teacherData.RoomId;
                ShortAnswerQuestionData saData = Adapting.getShortAnswerQuestionAsData(ques);
                rsContext.addActivity(saData);
                rsContext.SaveChanges();
                return RedirectToDashboard();
            }
            else
            {
                ques.RoomId = 0;
                ShortAnswerQuestionData saData = Adapting.getShortAnswerQuestionAsData(ques);
                rsContext.addActivity(saData);
                ExperimentData experimentData = (ExperimentData)rsContext.getActivity(ques.expID);
                saData.experimentOrder = experimentData.activities.Count;
                experimentData.addStep(saData);
                rsContext.SaveChanges();
                return RedirectToAction("EditExperiment", new { ExpID = ques.expID });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAmericanQuestion(AmericanQuestion ques)
        {

            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);

            if (ques.expID == -1)
            {
                ques.RoomId = teacherData.RoomId;
                AmericanQuestionData aqData = Adapting.getAmericanQuestionAsData(ques);
                rsContext.addActivity(aqData);
                rsContext.SaveChanges();
                return RedirectToDashboard();
            }

            else
            {
                ques.RoomId = 0;
                AmericanQuestionData aqData = Adapting.getAmericanQuestionAsData(ques);
                rsContext.addActivity(aqData);
                ExperimentData experimentData = (ExperimentData)rsContext.getActivity(ques.expID);
                aqData.experimentOrder = experimentData.activities.Count;
                experimentData.addStep(aqData);
                rsContext.SaveChanges();
                return RedirectToAction("EditExperiment", new { ExpID = ques.expID });
            }

        }


        // GET: Teacher/Display
        public ActionResult Display(long Id)
        {
            //Activity activity = cloneActivity(rsContext.getActivity(Id));
            //rsContext.addActivity(activity);
            ActivityData activityData = rsContext.getActivity(Id);
            if (activityData is ShortAnswerQuestionData)
            {
                return RedirectToAction("DisplayShortAnswerQuestion", new { ActivityId = activityData.id });
            }
            else if (activityData is TrueFalseQuestionData)
            {
                return RedirectToAction("DisplayTrueFalseQuestion", new { ActivityId = activityData.id });
            }
            else if (activityData is AmericanQuestionData)
            {
                return RedirectToAction("DisplayAmericanQuestion", new { ActivityId = activityData.id });
            }
            else if (activityData is ExperimentData)
            {
                ((ExperimentData)activityData).ActiveExpID = 0;
                TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
                List<StudentData> studentsInRoom = rsContext.getStudentsByTeacher(teacherData.RoomId);
                foreach (StudentData studentData in studentsInRoom)
                {
                    studentData.GroupID = 0;
                }
                //rsContext.addActiveExpLog(activeExpData); //TODO
                rsContext.SaveChanges();
                return RedirectToAction("DisplayExperiment", new { ActivityId = activityData.id });
            }
            return View();
        }

        public ActionResult GetUpdatedModelTrueFalseQuestion(long questionID)
        {
            TrueFalseQuestionData tfqData = (TrueFalseQuestionData)rsContext.getActivity(questionID);
            var info = new List<object>();
            var studentAnswers = new List<Object>();

            foreach (AnswerByPhoneData abpData in tfqData.studentsAnswers)
            {
                StudentData studentData = rsContext.getStudent(abpData.Phone, abpData.RoomID);
                studentAnswers.Add(new { Phone = abpData.Phone, Answer = abpData.Answer, FirstName = studentData.FirstName, LastName = studentData.LastName });
            }

            info.Add(new
            {
                AnswersCount = tfqData.studentsAnswers.Count,
                CountTrue = tfqData.counterTrue,
                CountFalse = tfqData.counterFalse,
                StudentAnswers = studentAnswers
            });

            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUpdatedModelAmericanQuestion(long questionID)
        {
            AmericanQuestionData aqData = (AmericanQuestionData)rsContext.getActivity(questionID);
            var info = new List<object>();
            var studentAnswers = new List<Object>();

            foreach (AnswerByPhoneData abpData in aqData.studentsAnswers)
            {
                StudentData studentData = rsContext.getStudent(abpData.Phone, abpData.RoomID);
                studentAnswers.Add(new { Phone = abpData.Phone, Answer = abpData.Answer, FirstName = studentData.FirstName, LastName = studentData.LastName });
            }

            info.Add(new
            {
                AnswersCount = aqData.studentsAnswers.Count,
                CountFirst = aqData.counterFirst,
                CountSecond = aqData.counterSecond,
                CountThird = aqData.counterThird,
                CountFourth = aqData.counterFourth,
                StudentAnswers = studentAnswers
            });

            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUpdatedModelShortAnswerQuestion(long questionID)
        {
            ShortAnswerQuestionData saqData = (ShortAnswerQuestionData)rsContext.getActivity(questionID);
            var info = new List<object>();
            var studentAnswers = new List<Object>();

            foreach (AnswerByPhoneData abpData in saqData.studentsAnswers)
            {
                StudentData studentData = rsContext.getStudent(abpData.Phone, abpData.RoomID);
                studentAnswers.Add(new { Phone = abpData.Phone, Answer = abpData.Answer, FirstName = studentData.FirstName, LastName = studentData.LastName });
            }

            info.Add(new
            {
                StudentAnswers = studentAnswers
            });

            return Json(info, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetGroupsAsJSON(long ActiveExpID)
        {
            ActiveExperimentData activeExpData = (ActiveExperimentData)rsContext.getActiveExperiment(ActiveExpID);
            var groupsProgress = new List<Object>();

            foreach (GroupData gData in rsContext.getGroupsByActiveExpID(ActiveExpID))
            {
                StudentData sData = gData.getfirstInStudents();
                if (sData == null)
                    return null;
                string groupName = sData.FirstName + " " + sData.LastName;


                var students = new List<Object>();
                foreach (StudentData sd in gData.Students)
                {
                    students.Add(" " + sd.FirstName + " " + sd.LastName);
                }


                var measurements = new List<Object>();
                MeasurementByGroupIdData md = gData.getLastInMeasurements();
                if (md == null) // no measurements yet
                {
                    groupsProgress.Add(new { GroupName = groupName, Progress = gData.Progress, GroupMeasurements = new List<Object>(), GroupStudents = students });
                    continue;
                }
                foreach (OneMeasureByGroupIdData om in md.measurements)
                { // TODO: PAY ATTENTION: THIS CODE IS ONLY FOR ONE PARAMETER!
                    measurements.Add(om.getFirstInMeasurements());
                }
                groupsProgress.Add(new { GroupName = groupName, Progress = gData.Progress, GroupMeasurements = measurements, GroupStudents = students });
            }

            return Json(groupsProgress, JsonRequestBehavior.AllowGet);
        }

        // This function used by the view "DisplayExperimentLog"
        public ActionResult GetDataForCollaborativeGraphAsJSON(long ActiveExpID)
        {
            ActiveExperimentData activeExpData = (ActiveExperimentData)rsContext.getActiveExperiment(ActiveExpID);
            var groupsProgress = new List<Object>();

            foreach (GroupData gData in rsContext.getGroupsByActiveExpID(ActiveExpID))
            {
                //StudentData sData = gData.getfirstInStudents();
                //if (sData == null)
                //    return null;
                var students = gData.studentListAsString;

                string groupName = students.Split(',')[0];

                var measurements = new List<Object>();
                MeasurementByGroupIdData md = gData.getLastInMeasurements();
                if (md == null) // no measurements yet
                {
                    groupsProgress.Add(new { GroupName = groupName, Progress = gData.Progress, GroupMeasurements = new List<Object>(), GroupStudents = students });
                    continue;
                }
                foreach (OneMeasureByGroupIdData om in md.measurements)
                { // TODO: PAY ATTENTION: THIS CODE IS ONLY FOR ONE PARAMETER!
                    measurements.Add(om.getFirstInMeasurements());
                }
                groupsProgress.Add(new { GroupName = groupName, Progress = gData.Progress, GroupMeasurements = measurements, GroupStudents = students });
            }

            return Json(groupsProgress, JsonRequestBehavior.AllowGet);
        }

        //POST: Teacher/DisplayShortAnswerQuestion
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DisplayShortAnswerQuestion(long ActivityId)
        {
            ShortAnswerQuestionData quesData = (ShortAnswerQuestionData)rsContext.getActivity(ActivityId);
            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            if (teacherData == null)
                throw new ArgumentNullException(nameof(teacherData));

            //RoomData roomData = teacherData.getRoomData();
            RoomData roomData = rsContext.getRoom(teacherData.RoomId);
            if (roomData == null)
                throw new ArgumentNullException(nameof(roomData));

            roomData.updateCurrentActivityId(quesData.id);
            rsContext.SaveChanges();
            ShortAnswerQuestion ques = Adapting.getShortAnswerQuestionFromData(quesData);

            return View(ques);
        }



        // POST: Teacher/DisplayTrueFalseQuestion
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DisplayTrueFalseQuestion(long ActivityId)
        {
            TrueFalseQuestionData quesData = (TrueFalseQuestionData)rsContext.getActivity(ActivityId);

            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            if (teacherData == null)
                throw new ArgumentNullException(nameof(teacherData));

            //RoomData roomData = teacherData.getRoomData();
            RoomData roomData = rsContext.getRoom(teacherData.RoomId);
            if (roomData == null)
                throw new ArgumentNullException(nameof(roomData));

            roomData.updateCurrentActivityId(quesData.id);
            rsContext.SaveChanges();

            TrueFalseQuestion ques = Adapting.getTrueFalseQuestionFromData(quesData);
            return View(ques);
        }


        // POST: Teacher/DisplayAmericanQuestion
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DisplayAmericanQuestion(long ActivityId)
        {
            AmericanQuestionData quesData = (AmericanQuestionData)rsContext.getActivity(ActivityId);
            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            if (teacherData == null)
                throw new ArgumentNullException(nameof(teacherData));

            //RoomData roomData = teacherData.getRoomData();
            RoomData roomData = rsContext.getRoom(teacherData.RoomId);
            if (roomData == null)
                throw new ArgumentNullException(nameof(roomData));

            roomData.updateCurrentActivityId(quesData.id);
            rsContext.SaveChanges();

            AmericanQuestion ques = Adapting.getAmericanQuestionFromData(quesData);

            return View(ques);
        }

        public ActionResult DisplayExperiment(long ActivityId)
        {
            ExperimentData expData = (ExperimentData)rsContext.getActivity(ActivityId);

            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            if (teacherData == null)
                throw new ArgumentNullException(nameof(teacherData));

            //RoomData roomData = teacherData.getRoomData();
            RoomData roomData = rsContext.getRoom(teacherData.RoomId);
            if (roomData == null)
                throw new ArgumentNullException(nameof(roomData));

            roomData.CurrentActivityId = ActivityId;
            rsContext.SaveChanges();

            return RedirectToAction("GroupSelection", new { ExpID = ActivityId });
        }

        public ActionResult GroupSelection(long ExpID)
        {
            TempData["ExpID"] = ExpID;
            return View();
        }

        public ActionResult StudentsProgress(ActiveExperiment ae, bool flag = false)
        {
            ActiveExperimentData aeData = Adapting.getActiveExperimentAsData(ae);
            ExperimentData expData = (ExperimentData)rsContext.getActivity(ae.ExpID);
            if (flag == false)
                rsContext.addActiveExperiment(aeData);
            expData.ActiveExpID = aeData.ActiveExpID;
            ae.ActiveExpID = aeData.ActiveExpID;
            rsContext.SaveChanges();
            TempData["NumberOfExperimentSteps"] = expData.activities.Count;
            TempData["DifferenceBetweenMeasures"] = 0;
            TempData["NumOfMeasures"] = 0;
            foreach (ActivityData act in expData.activities)
            {
                if (act is MeasurementData)
                {
                    MeasurementData measurement = (MeasurementData)act;
                    TempData["DifferenceBetweenMeasures"] = measurement.DifferenceBetweenMeasures;
                    TempData["NumOfMeasures"] = measurement.NumOfMeasures;
                }
            }
            return View("StudentsProgress", ae);
        }

        // GET: Teacher/CreateInstruction
        public ActionResult CreateInstruction(long ExpID)
        {
            TempData["ExpID"] = ExpID;
            return View();
        }

        // GET: Teacher/CreateImage
        public ActionResult CreateImage(long ExpID)
        {
            TempData["ExpID"] = ExpID;
            return View();
        }

        // GET: Teacher/CreateVideo
        public ActionResult CreateVideo(long ExpID)
        {
            TempData["ExpID"] = ExpID;
            return View();
        }

        // GET: Teacher/CreateInstruction
        public ActionResult CreateMeasurement(long ExpID)
        {
            TempData["ExpID"] = ExpID;
            return View();
        }


        public ActionResult RedirectToDashboard()
        {
            return RedirectToAction("Dashboard", "Teacher");
        }
    }
}