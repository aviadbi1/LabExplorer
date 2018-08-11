using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class TFQLogData : QLogData
    {
        public bool correctAnswerBool { get; set; }

        public int counterTrue { get; set; }

        public int counterFalse { get; set; }

        public TFQLogData() : base()
        {
        }

        public TFQLogData(TrueFalseQuestionData tfq, DateTime d) : base(tfq, d)
        {
            this.correctAnswerBool = tfq.correctAnswerBool;
            this.counterTrue = tfq.counterTrue;
            this.counterFalse = tfq.counterFalse;
        }

    }
}
