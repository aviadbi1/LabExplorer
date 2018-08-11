using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using System.ComponentModel.DataAnnotations;

namespace RoadScholar.Models
{
    public class TrueFalseQuestion : Question
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "correctAnswerStringRequired")]
        public bool correctAnswerBool { get; set; }

        public int counterTrue { get; set; }

        public int counterFalse { get; set; }
    }
}