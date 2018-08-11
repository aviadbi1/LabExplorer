using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class AmericanLog : QLog
    {
        public int correctAnswer { get; set; }
        public string firstAnswer { get; set; }
        public string secondAnswer { get; set; }
        public string thirdAnswer { get; set; }
        public string fourthAnswer { get; set; }

        public int counterFirst { get; set; }
        public int counterSecond { get; set; }
        public int counterThird { get; set; }
        public int counterFourth { get; set; }

    }
}