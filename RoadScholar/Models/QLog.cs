using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public abstract class QLog : ActivityLog
    {
        public string question { get; set; }

        public string explaination { get; set; }

        public virtual IList<AnswerByPhone> studentsAnswers { get; set; }

    }
}