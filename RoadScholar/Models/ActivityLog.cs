using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class ActivityLog
    {
        public long id { get; set; }

        // the name of the activity - required for the teacher to distinguish between them
        [Display(Name = "ActivityName", ResourceType = typeof(Resources.Resources))]
        public string ActivityName { get; set; }

        public bool isMainActivity { get; set; }

        public long expID { get; set; }

        public long RoomId { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Resources.Resources))]
        public DateTime date { get; set; }
        

    }
}