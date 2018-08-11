using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public abstract class Question : Activity
    {

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
             ErrorMessageResourceName = "questionStringRequired")]
        public string question { get; set; }

        public string explaination { get; set; }

        public virtual IList<AnswerByPhone> studentsAnswers { get; set; }
         
    }
}