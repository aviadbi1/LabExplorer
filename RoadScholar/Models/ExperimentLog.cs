using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class ExperimentLog : ActivityLog
    {
        public string ExperimentQuestion { get; set; }
        //public virtual IList<ActivityLogData> activities { get; set; }
        public long ActiveExpID { get; set; }
        public long NumOfMeasuresGraph { get; set; }
        public long DiffBetweenMeasuresGraph { get; set; }
    }
}