using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public abstract class QLogData : ActivityLogData
    {
        public string question { get; set; }

        public string explaination { get; set; }

        public virtual IList<AnswerByPhoneData> studentsAnswers { get; set; }

        public QLogData() : base(true)
        {
            studentsAnswers = new List<AnswerByPhoneData>();
        }

        public QLogData(QuestionData q, DateTime d) : base(q, d)
        {
            this.question = q.question;
            this.explaination = q.explaination;
            this.studentsAnswers = new List<AnswerByPhoneData>(q.studentsAnswers);
        }

    }
}
