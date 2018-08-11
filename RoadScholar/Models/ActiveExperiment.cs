using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class ActiveExperiment
    {
        public long ActiveExpID { get; set; }
        public long ExpID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "NumberOfGroupsRequired")]
        [Display(Name = "NumberOfGroups", ResourceType = typeof(Resources.Resources))]
        public int NumberOfGroups { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "MaxStudentPerGroupRequired")]
        [Display(Name = "MaxStudentsPerGroup", ResourceType = typeof(Resources.Resources))]
        public int MaxStudentPerGroup { get; set; }

        public int NumberCreatedGroups { get; set; }
    }
}