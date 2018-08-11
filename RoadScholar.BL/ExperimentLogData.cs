using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class ExperimentLogData : ActivityLogData
    {
        public string ExperimentQuestion { get; set; }
        //public virtual IList<ActivityLogData> activities { get; set; }
        public long ActiveExpID { get; set; }
        public long NumOfMeasuresGraph { get; set; }
        public long DiffBetweenMeasuresGraph { get; set; }

        public ExperimentLogData() : base()
        {
        }

        public ExperimentLogData(long ActiveExpID) : base(true)
        {
            this.expID = -1;
            this.ActiveExpID = ActiveExpID;
            //this.ActiveExpID = 0;
            //this.Type = "Experiment";
        }

        public ExperimentLogData(ExperimentData e, DateTime d, long NumOfMeasuresGraph, long DiffBetweenMeasuresGraph) : base(e, d)
        {
            this.expID = -1;
            this.ActiveExpID = e.ActiveExpID;
            this.NumOfMeasuresGraph = NumOfMeasuresGraph;
            this.DiffBetweenMeasuresGraph = DiffBetweenMeasuresGraph;
            this.ExperimentQuestion = e.ExperimentQuestion;
        }

    }
}
