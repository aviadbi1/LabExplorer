using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class ShortAnswerQuestion : Question
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "correctAnswerStringRequired")]
        public string correctAnswerString { get; set; }
    }
}