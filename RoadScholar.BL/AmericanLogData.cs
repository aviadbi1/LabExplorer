using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class AmericanLogData : QLogData
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

        public AmericanLogData() : base()
        {
        }

        public AmericanLogData(AmericanQuestionData aq, DateTime d) : base(aq, d)
        {
            this.correctAnswer = aq.correctAnswer;
            this.firstAnswer = aq.firstAnswer;
            this.secondAnswer = aq.secondAnswer;
            this.thirdAnswer = aq.thirdAnswer;
            this.fourthAnswer = aq.fourthAnswer;
            this.counterFirst = aq.counterFirst;
            this.counterSecond = aq.counterSecond;
            this.counterThird = aq.counterThird;
            this.counterFourth = aq.counterFourth;
        }
    }
}
