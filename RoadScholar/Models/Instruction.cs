using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class Instruction : Activity
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "CommandRequired")]
        public string Command { get; set; }
    }
}