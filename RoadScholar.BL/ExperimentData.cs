using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    //[Table("Experiments")]
    public class ExperimentData : ActivityData
    {

        public string ExperimentQuestion { get; set; }
        public virtual IList<ActivityData> activities { get; set; }
        public long ActiveExpID { get; set; }

        public ExperimentData() : base(true)
        {
            this.activities = new List<ActivityData>();
            this.expID = -1;
            this.ActiveExpID = 0;
            this.Type = "Experiment";
        }

        public ExperimentData(ExperimentData exp) : base(exp)
        {
            this.expID = -1;
            this.ExperimentQuestion = exp.ExperimentQuestion;
            this.Type = "Experiment";
            RoadScholarContext rsContext = new RoadScholarContext();
            this.activities = new List<ActivityData>();
            foreach (ActivityData act in exp.activities)
            {
                ActivityData copiedActivity = null;
                if (act is TrueFalseQuestionData)
                {
                    copiedActivity = new TrueFalseQuestionData((TrueFalseQuestionData)act);
                    rsContext.addActivity(copiedActivity);
                }
                else if (act is AmericanQuestionData)
                {
                    copiedActivity = new AmericanQuestionData((AmericanQuestionData)act);
                    rsContext.addActivity(copiedActivity);
                }
                else if (act is ShortAnswerQuestionData)
                {
                    copiedActivity = new ShortAnswerQuestionData((ShortAnswerQuestionData)act);
                    rsContext.addActivity(copiedActivity);
                }
                else if (act is InstructionData)
                {
                    copiedActivity = new InstructionData((InstructionData)act);
                    rsContext.addActivity(copiedActivity);
                }
                else if (act is ImageData)
                {
                    copiedActivity = new ImageData((ImageData)act);
                    rsContext.addActivity(copiedActivity);
                }
                else if (act is VideoData)
                {
                    copiedActivity = new VideoData((VideoData)act);
                    rsContext.addActivity(copiedActivity);
                }
                else if (act is MeasurementData)
                {
                    copiedActivity = new MeasurementData((MeasurementData)act);
                    rsContext.addActivity(copiedActivity);
                }
                this.activities.Add(copiedActivity);
            }
        }

        public void addStep(ActivityData activity)
        {
            RoadScholarContext rsContext = new RoadScholarContext();
            activities.Add(activity);
            rsContext.SaveChanges();
        }

        public long getNumOfMeasures()
        {
            long ans = -1;

            // We return NumOfMeasures from the last measurement set
            foreach(ActivityData ad in activities){
                if(ad is MeasurementData)
                {
                    ans = ((MeasurementData)ad).NumOfMeasures;
                }
            }

            return ans;
        }

        public long getDiffBetweenMeasures()
        {
            long ans = -1;

            // We return NumOfMeasures from the last measurement set
            foreach (ActivityData ad in activities)
            {
                if (ad is MeasurementData)
                {
                    ans = ((MeasurementData)ad).DifferenceBetweenMeasures;
                }
            }

            return ans;
        }
    }
}
