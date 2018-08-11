using RoadScholar.BL;
using RoadScholar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoadScholar.Controllers
{
    [Authorize]
    public class ActivitiesController : BaseController
    {

        RoadScholarContext rsContext = new RoadScholarContext();

        // GET: Activities/ExistingActivities
        public ActionResult ExistingActivities()
        {
            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            return View(Adapting.getActivityListFromData(rsContext.getMainActivities(teacherData.RoomId)));
        }

        public ActionResult DeleteActivity(long Id)
        {
            ActivityData activity = rsContext.getActivity(Id);
            TempData["infoMessage"] = Resources.Resources.TheActivityHaveBeenDeletedSuccessfully + activity.ActivityName;
            rsContext.DeleteActivity(Id);
            return RedirectToAction("ExistingActivities");
        }

        public ActionResult DeleteActivityHistory(long Id)
        {
            ActivityLogData activityLog = rsContext.getActivityLog(Id);
            TempData["infoMessage"] = Resources.Resources.TheActivityHistoryHaveBeenDeletedSuccessfullyForActivity + activityLog.ActivityName + " " + activityLog.date;
            rsContext.DeleteActivityHistory(Id);
            return RedirectToAction("HistoryByActivity");
        }

        public ActionResult DeleteAllActivitiesOfStudent(string phoneNumber, long RoomID)
        {
            StudentData sd = rsContext.getStudent(phoneNumber, RoomID);
            TempData["infoMessage"] = Resources.Resources.TheActivitiesHistoryHaveBeenDeletedSuccessfullyForStudent + sd.FirstName + " " + sd.LastName;
            rsContext.DeleteAllActivitiesOfStudent(phoneNumber, RoomID);
            return RedirectToAction("HistoryByStudent");
        }



        // GET: Activities/ActivityLogs
        public ActionResult ActivityLogs()
        {
            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            List<ActivityLogData> lstData = rsContext.getHistory(teacherData.RoomId);
            List<ActivityLog> lst = Adapting.getActivityLogListFromData(lstData);
            return View(lst);
        }

        // GET: Activities/HistoryByActivity
        public ActionResult HistoryByActivity()
        {
            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            return View(Adapting.getActivityLogListFromData(rsContext.getHistory(teacherData.RoomId)));
        }

        // GET: Activities/HistoryByStudent
        public ActionResult HistoryByStudent()
        {
            TeacherData teacherData = rsContext.getTeacher(User.Identity.Name);
            TempData["RoomID"] = teacherData.RoomId;
            return View(Adapting.getStudentListFromData(rsContext.getStudentsByTeacher(teacherData.RoomId)));
        }

        // GET: Activities/HistoryByStudent
        public ActionResult DisplayActivitiesOfStudent(string phoneNumber, long RoomID)
        {
            TempData["PhoneNumber"] = phoneNumber;
            TempData["RoomID"] = RoomID;
            return View(Adapting.getActivityListFromData(rsContext.getActivitiesByStudentPhone(phoneNumber, RoomID)));
        }

        // GET: Activities/DisplayLogByActivityIDAndStudentPhone
        public ActionResult DisplayLogByActivityIDAndStudentPhone(long ActivityID, string StudentPhone)
        {
            ActivityData activityData = rsContext.getActivity(ActivityID);
            if (activityData is TrueFalseQuestionData)
            {
                return RedirectToAction("DisplayTFQLogForStudent", new { tfqID = ActivityID, StudentPhone = StudentPhone });
            }
            else if (activityData is AmericanQuestionData)
            {
                return RedirectToAction("DisplayAmericanQLogForStudent", new { aqID = ActivityID, StudentPhone = StudentPhone });
            }
            else if (activityData is ShortAnswerQuestionData)
            {
                return RedirectToAction("DisplaySAQLogForStudent", new { saqID = ActivityID, StudentPhone = StudentPhone });
            }
            else
            {
                // TODO experiment
            }

            return null;
        }

        // GET: Activities/DisplayTFQLogForStudent
        public ActionResult DisplayTFQLogForStudent(long tfqID, string StudentPhone)
        {
            ActivityData activityData = rsContext.getActivity(tfqID);
            TrueFalseQuestion tfq = Adapting.getTrueFalseQuestionFromData((TrueFalseQuestionData)activityData);

            AnswerByPhone answer = Adapting.getAnswerByPhoneFromData(rsContext.getAnswerByPhoneAndActivityID(tfq.id, StudentPhone, tfq.RoomId));
            Student student = Adapting.getStudentFromData(rsContext.getStudent(StudentPhone, answer.RoomID));

            TempData["StudentAnswer"] = answer.Answer;
            TempData["StudentName"] = student.FirstName + " " + student.LastName;

            return View(tfq);
        }

        // GET: Activities/DisplayAmericanQLogForStudent
        public ActionResult DisplayAmericanQLogForStudent(long aqID, string StudentPhone)
        {
            ActivityData activityData = rsContext.getActivity(aqID);
            AmericanQuestion aq = Adapting.getAmericanQuestionFromData((AmericanQuestionData)activityData);

            AnswerByPhone answer = Adapting.getAnswerByPhoneFromData(rsContext.getAnswerByPhoneAndActivityID(aq.id, StudentPhone, aq.RoomId));
            Student student = Adapting.getStudentFromData(rsContext.getStudent(StudentPhone, answer.RoomID));

            TempData["StudentAnswer"] = answer.Answer;
            TempData["StudentName"] = student.FirstName + " " + student.LastName;

            return View(aq);
        }

        // GET: Activities/DisplaySAQLogForStudent
        public ActionResult DisplaySAQLogForStudent(long saqID, string StudentPhone)
        {
            ActivityData activityData = rsContext.getActivity(saqID);
            ShortAnswerQuestion saq = Adapting.getShortAnswerQuestionFromData((ShortAnswerQuestionData)activityData);

            AnswerByPhone answer = Adapting.getAnswerByPhoneFromData(rsContext.getAnswerByPhoneAndActivityID(saq.id, StudentPhone, saq.RoomId));
            Student student = Adapting.getStudentFromData(rsContext.getStudent(StudentPhone, answer.RoomID));

            TempData["StudentAnswer"] = answer.Answer;
            TempData["StudentName"] = student.FirstName + " " + student.LastName;

            return View(saq);
        }


        // GET: Activities/DisplayLog
        public ActionResult DisplayLog(long Id)
        {
            ActivityLogData activityLogData = rsContext.getActivityLog(Id);

            if (activityLogData is SALogData)
            {
                return RedirectToAction("DisplaySALog", new { ActivityId = activityLogData.id });
            }
            else if (activityLogData is TFQLogData)
            {
                return RedirectToAction("DisplayTFQLog", new { ActivityId = activityLogData.id });
            }
            else if (activityLogData is AmericanLogData)
            {
                return RedirectToAction("DisplayAmericanLog", new { ActivityId = activityLogData.id });
            }
            else if (activityLogData is ExperimentLogData)
            {
                return RedirectToAction("DisplayExperimentLog", new { ActivityId = activityLogData.id });
            }
            return View();
        }

        // GET: Activities/DisplayTFQLog
        public ActionResult DisplayTFQLog(long ActivityId)
        {
            TFQLogData tfqlData = (TFQLogData)rsContext.getActivityLog(ActivityId);
            TFQLog tfql = Adapting.getTFQLogFromData(tfqlData);
            return View(tfql);
        }

        // GET: Activities/DisplayAmericanLog
        public ActionResult DisplayAmericanLog(long ActivityId)
        {
            AmericanLogData aqlData = (AmericanLogData)rsContext.getActivityLog(ActivityId);
            AmericanLog aql = Adapting.getAmericanLogFromData(aqlData);
            return View(aql);
        }

        // GET: Activities/DisplaySALog
        public ActionResult DisplaySALog(long ActivityId)
        {
            SALogData saqlData = (SALogData)rsContext.getActivityLog(ActivityId);
            SALog saql = Adapting.getShortAnswerQuestionLogFromData(saqlData);
            return View(saql);
        }

        // GET: Activities/DisplayExperimentLog
        public ActionResult DisplayExperimentLog(long ActivityId)
        {
            ExperimentLogData elData = (ExperimentLogData)rsContext.getActivityLog(ActivityId);
            ExperimentLog el = Adapting.getExperimentLogFromData(elData);
            return View(el);
        }

    }
}