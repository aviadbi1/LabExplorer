using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class QuestionData : ActivityData
    {

        public string question { get; set; }

        public string explaination { get; set; }

        public virtual IList<AnswerByPhoneData> studentsAnswers { get; set; }

        public QuestionData() : base(true)
        {
            studentsAnswers = new List<AnswerByPhoneData>();
        }

        public QuestionData(QuestionData q) : base(q)
        {
            this.question = q.question;
            this.explaination = q.explaination;
        }

        public void AddAnswerToDictionary(long activityId, string studentPhone, string studentAnswer, long RoomID)
        {
            foreach (AnswerByPhoneData abp in studentsAnswers)
            {
                if (abp.Phone == studentPhone)
                {
                    return;
                }
            }
            ((List<AnswerByPhoneData>)studentsAnswers).Add(new AnswerByPhoneData(activityId, studentPhone, studentAnswer, RoomID));
        }

    }
}
