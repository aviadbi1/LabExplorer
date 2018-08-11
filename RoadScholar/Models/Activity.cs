using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace RoadScholar.Models
{
    public class Activity
    {
        public long id { get; set; }

        // the name of the activity - required for the teacher to distinguish between them
        [Display(Name = "ActivityName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                   ErrorMessageResourceName = "ActivityNameRequired")]
        //[StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
        //                      ErrorMessageResourceName = "ActivityNameLong")]
        public string ActivityName { get; set; }

        public bool isMainActivity { get; set; }

        public long expID { get; set; }

        public long RoomId { get; set; }

        public string Type { get; set; }

        public int experimentOrder { get; set; }

    }
}