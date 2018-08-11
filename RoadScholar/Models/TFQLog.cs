using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class TFQLog : QLog
    {
        public bool correctAnswerBool { get; set; }

        public int counterTrue { get; set; }

        public int counterFalse { get; set; }

    }
}