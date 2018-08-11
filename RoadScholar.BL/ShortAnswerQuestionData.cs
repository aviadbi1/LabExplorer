using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class ShortAnswerQuestionData : QuestionData
    {
        public string correctAnswerString { get; set; }

        public ShortAnswerQuestionData() : base()
        {
            this.Type = "ShortAnswerQuestion";
        }

        public ShortAnswerQuestionData(ShortAnswerQuestionData saq) : base(saq)
        {
            this.correctAnswerString = saq.correctAnswerString;
        }

        public void reset()
        {       
            studentsAnswers = new List<AnswerByPhoneData>();
        }
    }
}
