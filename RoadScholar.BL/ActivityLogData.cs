using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    [Table("ActivityLogs")]
    public abstract class ActivityLogData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        // the name of the activity - required for the teacher to distinguish between them
        public string ActivityName { get; set; }

        public bool isMainActivity { get; set; }

        public long expID { get; set; }

        public long RoomId { get; set; }

        public DateTime date { get; set; }

        public ActivityLogData()
        { }

        public ActivityLogData(bool isMainActivity)
        {
            this.isMainActivity = isMainActivity;
        }

        public ActivityLogData(ActivityData a, DateTime d)
        {
            this.ActivityName = a.ActivityName;
            this.isMainActivity = a.isMainActivity;
            this.expID = a.expID;
            this.RoomId = a.RoomId;
            this.date = d;
        }

        public ActivityLogData(string activityName)
        {
            this.ActivityName = activityName;
            this.isMainActivity = false;
        }

    }
}
