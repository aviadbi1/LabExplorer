using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System;
using System.Collections.Generic;

namespace RoadScholar.Models
{
    //// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}

    //public class RoadScholarContext : IdentityDbContext<ApplicationUser>
    //{
    //    public RoadScholarContext() : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    public static RoadScholarContext Create()
    //    {
    //        return new RoadScholarContext();
    //    }

    //    public DbSet<RoadScholar.Models.Teacher> Teachers { get; set; }
    //    public DbSet<RoadScholar.Models.Room> Rooms { get; set; }
    //    public DbSet<RoadScholar.Models.Activity> Activities { get; set; }
    //    public DbSet<RoadScholar.Models.Student> Students { get; set; }
    //    public DbSet<RoadScholar.Models.Experiment> Experiments { get; set; }
    //    public DbSet<RoadScholar.Models.AnswerByPhone> AnswersByPhone { get; set; }
    //    public DbSet<RoadScholar.Models.ActiveExperiment> ActiveExperiments { get; set; }
    //    public DbSet<RoadScholar.Models.ActivityLog> ActivityLogs { get; set; }




    //    public Teacher getTeacher(string email)
    //    {
    //        return Teachers.SingleOrDefault(t => t.Email == email);
    //    }

    //    public void addTeacher(Teacher teacher)
    //    {
    //        Teachers.Add(teacher);
    //        SaveChanges();
    //    }

    //    public Room getRoom(long id)
    //    {
    //        return Rooms.SingleOrDefault(r => r.id == id);
    //    }

    //    public void addRoom(Room room)
    //    {
    //        Rooms.Add(room);
    //        SaveChanges();
    //    }

    //    public Activity getActivity(long id)
    //    {
    //        return Activities.SingleOrDefault(a => a.id == id);
    //    }

    //    public Activity getActivity(string activityName)
    //    {
    //        return Activities.SingleOrDefault(a => a.ActivityName == activityName);
    //    }

    //    public ActivityLog getActivityLog(long id)
    //    {
    //        return ActivityLogs.SingleOrDefault(a => a.id == id);
    //    }

    //    public void addActivity(Activity activity)
    //    {
    //        Activities.Add(activity);
    //        SaveChanges();
    //    }

    //    public void addActivityLog(ActivityLog activityLog)
    //    {
    //        ActivityLogs.Add(activityLog);
    //        SaveChanges();
    //    }

    //    public Student getStudent(string phoneNumber)
    //    {
    //        return Students.SingleOrDefault(s => s.PhoneNumber == phoneNumber);
    //    }

    //    public void addStudent(Student student)
    //    {
    //        Students.Add(student);
    //        SaveChanges();
    //    }

    //    public List<Activity> getMainActivities(long roomID)
    //    {
    //        List<Activity> lst = new List<Activity>();
    //        List<Activity> allMainActivities = Activities.Where(act => act.isMainActivity == true).ToList();
    //        foreach (Activity act in allMainActivities)
    //        {
    //            if (act.RoomId == roomID)
    //            {
    //                lst.Add(act);
    //            }
    //            else if (act.Type == "Experiment" && act.RoomId == 0)
    //            {
    //                if (allMainActivities.Where(otherAct => otherAct.Type == "Experiment" && act.ActivityName == otherAct.ActivityName && otherAct.RoomId == roomID).ToList().Count == 0)
    //                {
    //                    Experiment copiedExp = new Experiment((Experiment)act);
    //                    copiedExp.RoomId = roomID;
    //                    addActivity(copiedExp);
    //                    lst.Add(copiedExp);
    //                }

    //            }
    //        }
    //        return lst;
    //    }


    //    public List<ActivityLog> getHistory(long roomID)
    //    {
    //        List<ActivityLog> lst = new List<ActivityLog>();
    //        List<ActivityLog> allHistory = ActivityLogs.ToList();
    //        foreach (TFQLog act in allHistory)
    //        {
    //            if (act.RoomId == roomID)
    //            {
    //                lst.Add(act);
    //            }
    //        }
    //        return lst;
    //    }

    //    public void setActivityIDtoRoom(long activityId, long roomID)
    //    {
    //        Room room = Rooms.SingleOrDefault(r => r.id == roomID);
    //        room.CurrentActivityId = activityId;
    //        SaveChanges();
    //    }

    //    internal void addActiveExperiment(ActiveExperiment ae)
    //    {
    //        ActiveExperiments.Add(ae);
    //        SaveChanges();
    //    }
    //}
}