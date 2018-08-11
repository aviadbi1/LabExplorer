using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class SALogData : QLogData
    {
        public string correctAnswerString { get; set; }

        public SALogData() : base()
        {
        }

        public SALogData(ShortAnswerQuestionData saq, DateTime d) : base(saq, d)
        {
            this.correctAnswerString = saq.correctAnswerString;
        }
    }
}
