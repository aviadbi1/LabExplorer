using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class OneMeasureByGroupId
    {
        public long id { get; set; }
        public long MeasurementByGroupIDid { get; set; }
        // This collection size is the number of the parameters
        public virtual IList<double> measurements { get; set; }

    }
}