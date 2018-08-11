using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class AmericanQuestion : Question
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
             ErrorMessageResourceName = "correctAnswerStringRequired")]
        public int correctAnswer { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "AnswerStringRequired")]
        public string firstAnswer { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "AnswerStringRequired")]
        public string secondAnswer { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "AnswerStringRequired")]
        public string thirdAnswer { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "AnswerStringRequired")]
        public string fourthAnswer { get; set; }

        public int counterFirst { get; set; }
        public int counterSecond { get; set; }
        public int counterThird { get; set; }
        public int counterFourth { get; set; }
    }
}