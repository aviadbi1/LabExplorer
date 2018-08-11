using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System;
using System.Collections.Generic;

namespace RoadScholar.BL
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class RoadScholarContext : IdentityDbContext<ApplicationUser>
    {
        public RoadScholarContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static RoadScholarContext Create()
        {
            return new RoadScholarContext();
        }

        public DbSet<TeacherData> Teachers { get; set; }
        public DbSet<RoomData> Rooms { get; set; }
        public DbSet<ActivityData> Activities { get; set; }
        public DbSet<StudentData> Students { get; set; }
        public DbSet<ExperimentData> Experiments { get; set; }
        public DbSet<AnswerByPhoneData> AnswersByPhone { get; set; }
        public DbSet<ActiveExperimentData> ActiveExperiments { get; set; }
        public DbSet<ActivityLogData> ActivityLogs { get; set; }
        public DbSet<GroupData> Groups { get; set; }
        public DbSet<MeasurementData> Measurements { get; set; }
        public DbSet<MeasurementByGroupIdData> MeasurementsByGroupId { get; set; }
        public DbSet<OneMeasureByGroupIdData> OneMeasuresByGroupId { get; set; }
        public DbSet<OneMeasurementEvalData> OneMeasuresEvalData { get; set; }
        public DbSet<MeasureInstructionData> MeasureInstructionsData { get; set; }


        public void clearAllTables()
        {
            Groups.RemoveRange(Groups);
            AnswersByPhone.RemoveRange(AnswersByPhone);
            //Rooms.RemoveRange(Rooms);
            //Teachers.RemoveRange(Teachers);
            MeasureInstructionsData.RemoveRange(MeasureInstructionsData);
            OneMeasuresEvalData.RemoveRange(OneMeasuresEvalData);
            OneMeasuresByGroupId.RemoveRange(OneMeasuresByGroupId);
            MeasurementsByGroupId.RemoveRange(MeasurementsByGroupId);
            Measurements.RemoveRange(Measurements);
            ActiveExperiments.RemoveRange(ActiveExperiments);
            Experiments.RemoveRange(Experiments);
            Activities.RemoveRange(Activities);
            ActivityLogs.RemoveRange(ActivityLogs);
            //Students.RemoveRange(Students);

            SaveChanges();
        }

        public TeacherData getTeacher(string email)
        {
            return Teachers.SingleOrDefault(t => t.Email == email);
        }

        public void addTeacher(TeacherData teacher)
        {
            Teachers.Add(teacher);
            SaveChanges();
        }

        public RoomData getRoom(long id)
        {
            return Rooms.SingleOrDefault(r => r.id == id);
        }

        public void addRoom(RoomData room)
        {
            Rooms.Add(room);
            SaveChanges();
        }

        public ActivityData getActivity(long id)
        {
            return Activities.SingleOrDefault(a => a.id == id);
        }

        public ActiveExperimentData getActiveExperiment(long id)
        {
            return ActiveExperiments.SingleOrDefault(a => a.ActiveExpID == id);

        }

        public ActivityData getActivity(string activityName)
        {
            return Activities.SingleOrDefault(a => a.ActivityName == activityName && a.RoomId == 0);
        }

        public ActivityLogData getActivityLog(long id)
        {
            return ActivityLogs.SingleOrDefault(a => a.id == id);
        }

        public void addActivity(ActivityData activity)
        {
            Activities.Add(activity);
            SaveChanges();
        }

        public void addActivityLog(ActivityLogData activityLog)
        {
            ActivityLogs.Add(activityLog);
            SaveChanges();
        }

        public StudentData getStudent(string phoneNumber, long roomId)
        {
            return Students.SingleOrDefault(s => s.PhoneNumber == phoneNumber && s.RoomId == roomId);
        }

        public void addStudent(StudentData student)
        {
            Students.Add(student);
            SaveChanges();
        }

        public List<ActivityData> getMainActivities(long roomID)
        {
            List<ActivityData> lst = new List<ActivityData>();
            List<ActivityData> allMainActivities = Activities.Where(act => act.isMainActivity == true).ToList();
            foreach (ActivityData act in allMainActivities)
            {
                if (act.RoomId == roomID)
                {
                    lst.Add(act);
                }
                else if (act.Type == "Experiment" && act.RoomId == 0)
                {
                    if (allMainActivities.Where(otherAct => otherAct.Type == "Experiment" &&
                    act.ActivityName == otherAct.ActivityName && otherAct.RoomId == roomID).ToList().Count == 0)
                    {
                        if (((ExperimentData)act).activities.Count != 0)
                        {
                            ExperimentData copiedExp = new ExperimentData((ExperimentData)act);
                            copiedExp.RoomId = roomID;
                            addActivity(copiedExp);
                            lst.Add(copiedExp);
                        }
                    }

                }
            }
            return lst;
        }


        public List<ActivityLogData> getHistory(long roomID)
        {
            List<ActivityLogData> lst = new List<ActivityLogData>();
            List<ActivityLogData> allHistory = ActivityLogs.ToList();
            foreach (ActivityLogData act in allHistory)
            {
                if (act.RoomId == roomID)
                {
                    lst.Add(act);
                }
            }
            return lst;
        }

        public List<StudentData> getStudentsByTeacher(long roomID)
        {
            List<StudentData> lstData = new List<StudentData>();
            List<StudentData> allStudents = Students.ToList();
            foreach (StudentData sData in allStudents)
            {
                if (sData.RoomId == roomID)
                {
                    lstData.Add(sData);
                }
            }
            return lstData;
        }

        public List<ActivityData> getActivitiesByStudentPhone(string studentPhone, long RoomID)
        {
            List<ActivityData> lstData = new List<ActivityData>();
            List<AnswerByPhoneData> allAnswersByPhone = AnswersByPhone.ToList();

            // Add all the questions activities of a student
            foreach (AnswerByPhoneData sData in allAnswersByPhone)
            {
                if (sData.Phone == studentPhone && sData.RoomID == RoomID)
                {
                    ActivityData ad = getActivity(sData.ActivityId);
                    if (ad.expID == -1)
                        lstData.Add(ad);
                }
            }

            // TODO Add all the experiments activities of a student

            return lstData;
        }

        public void setActivityIDtoRoom(long activityId, long roomID)
        {
            RoomData room = Rooms.SingleOrDefault(r => r.id == roomID);
            room.CurrentActivityId = activityId;
            SaveChanges();
        }

        public void addActiveExperiment(ActiveExperimentData ae)
        {
            ActiveExperiments.Add(ae);
            SaveChanges();
        }

        public AnswerByPhoneData getAnswerByPhoneAndActivityID(long ActivityID, string StudentPhone, long RoomId)
        {
            List<AnswerByPhoneData> allAnswersByPhone = AnswersByPhone.ToList();

            foreach (AnswerByPhoneData sData in allAnswersByPhone)
            {
                if (sData.ActivityId == ActivityID && sData.Phone == StudentPhone && sData.RoomID == RoomId)
                {
                    return sData;
                }
            }

            return null;
        }

        public GroupData getGroupById(long groupID)
        {
            return Groups.SingleOrDefault(g => g.GroupID == groupID);
        }

        public void addGroup(GroupData groupData)
        {
            Groups.Add(groupData);
            SaveChanges();
        }

        public IEnumerable<GroupData> getGroupsByActiveExpID(long activeExpID)
        {
            List<GroupData> groupsData = Groups.Where(group => group.ActiveExpID == activeExpID).ToList();
            return groupsData;
        }

        public void DeleteActivity(long actId)
        {
            ActivityData activity = getActivity(actId);
            Activities.Remove(activity);
            SaveChanges();
        }

        public void DeleteActivityHistory(long logId)
        {
            ActivityLogData log = getActivityLog(logId);
            if (log is QLogData)
            {
                ICollection<AnswerByPhoneData> answersData = ((QLogData)log).studentsAnswers;
                deleteAnswers(answersData);
            }
            ActivityLogs.Remove(log);
            SaveChanges();
        }

        public void deleteAnswers(ICollection<AnswerByPhoneData> answersData)
        {
            if (answersData.Count == 0)
                return;

            AnswersByPhone.Remove(answersData.First());
            deleteAnswers(answersData);
        }

        public void DeleteAllActivitiesOfStudent(string phoneNumber, long roomID)
        {
            List<AnswerByPhoneData> answersData = AnswersByPhone.Where(record => record.Phone == phoneNumber && record.RoomID == roomID).ToList();
            foreach (AnswerByPhoneData answer in answersData)
            {
                answer.RoomID = -1;
            }
            SaveChanges();
        }
        public bool studentHasActivities(string phoneNumber, long roomId)
        {
            List<AnswerByPhoneData> answersData = AnswersByPhone.Where(record => record.Phone == phoneNumber && record.RoomID == roomId).ToList();
            if (answersData.Count != 0)
                return true;
            return false;
        }

        public void AddMeasurementByGroupID(MeasurementByGroupIdData mbgid)
        {
            MeasurementsByGroupId.Add(mbgid);
            SaveChanges();
        }

        public void AddOneMeasureByGroupID(OneMeasureByGroupIdData oneM)
        {
            OneMeasuresByGroupId.Add(oneM);
            SaveChanges();
        }

        public MeasurementData getMeasurementByID(long measurementID)
        {
            return Measurements.SingleOrDefault(m => m.id == measurementID);
        }

        public MeasurementByGroupIdData GetMeasurementByGroupID(long expID, long roomId, long groupID, long currActivityIndex)
        {
            return MeasurementsByGroupId.SingleOrDefault(m => m.expID == expID && m.RoomId == roomId &&
                                                        m.GroupID == groupID && m.CurrentActivityIndex == currActivityIndex);
        }
    }
}