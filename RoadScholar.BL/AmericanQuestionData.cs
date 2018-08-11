using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class AmericanQuestionData : QuestionData
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

        public AmericanQuestionData() : base()
        {
            this.Type = "AmericanQuestion";
        }

        public AmericanQuestionData(AmericanQuestionData aq) : base(aq)
        {
            this.correctAnswer = aq.correctAnswer;
            this.firstAnswer = aq.firstAnswer;
            this.secondAnswer = aq.secondAnswer;
            this.thirdAnswer = aq.thirdAnswer;
            this.fourthAnswer = aq.fourthAnswer;
            this.counterFirst = 0;
            this.counterSecond = 0;
            this.counterThird = 0;
            this.counterFourth = 0;
        }

        public void updateCounters(string studentPhone, int studentAnswer)
        {
            foreach (AnswerByPhoneData abp in studentsAnswers)
            {
                if (abp.Phone == studentPhone)
                {
                    return;
                }
            }

            if (studentAnswer == 1)
            {
                counterFirst++;
            }
            else if (studentAnswer == 2)
            {
                counterSecond++;
            }
            else if (studentAnswer == 3)
            {
                counterThird++;
            }
            else if (studentAnswer == 4)
            {
                counterFourth++;
            }
        }

        public void reset()
        {
            counterFirst = 0;
            counterSecond = 0;
            counterThird = 0;
            counterFourth = 0;
            studentsAnswers = new List<AnswerByPhoneData>();
        }
    }
}
