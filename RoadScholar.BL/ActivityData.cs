using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    [Table("Activities")]
    public class ActivityData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        // the name of the activity - required for the teacher to distinguish between them
        public string ActivityName { get; set; }

        public bool isMainActivity { get; set; }

        public long expID { get; set; }

        public long RoomId { get; set; }

        public string Type { get; set; }

        public int experimentOrder { get; set; }


        public ActivityData()
        { }

        public ActivityData(bool isMainActivity)
        {
            this.isMainActivity = isMainActivity;
        }

        public ActivityData(ActivityData a)
        {
            this.ActivityName = a.ActivityName;
            this.isMainActivity = a.isMainActivity;
            this.expID = a.expID;
            this.RoomId = a.RoomId;
            this.Type = a.Type;
            this.experimentOrder = a.experimentOrder;
        }

        public ActivityData(string activityName)
        {
            this.ActivityName = activityName;
            this.isMainActivity = false;
            this.experimentOrder = 0;
        }

    }
}
