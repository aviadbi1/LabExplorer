using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class TrueFalseQuestionData : QuestionData
    {
        public bool correctAnswerBool { get; set; }

        public int counterTrue { get; set; }

        public int counterFalse { get; set; }

        public TrueFalseQuestionData() : base()
        {
            this.Type = "TrueFalseQuestion";
        }

        public TrueFalseQuestionData(TrueFalseQuestionData tfq) : base(tfq)
        {
            this.correctAnswerBool = tfq.correctAnswerBool;
            this.counterTrue = 0;
            this.counterFalse = 0;
        }

        public void updateCounters(string studentPhone, string studentAnswer)
        {
            foreach (AnswerByPhoneData abp in studentsAnswers)
            {
                if (abp.Phone == studentPhone)
                {
                    return;
                }
            }

            if (studentAnswer == "Yes")
            {
                counterTrue++;
            }
            else
            {
                counterFalse++;
            }
        }

        public void reset()
        {
            counterTrue = 0;
            counterFalse = 0;
            studentsAnswers = new List<AnswerByPhoneData>();
        }


    }
}
