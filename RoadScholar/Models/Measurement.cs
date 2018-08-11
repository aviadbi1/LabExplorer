using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class Measurement : Activity
    {
        public long NumOfMeasures { get; set; }
        public long DifferenceBetweenMeasures { get; set; }
        public long NumOfParametersToMeasure { get; set; }
        public long WindowOpenTimeSeconds { get; set; }
        // if measurementInstructions.Count == 1 -> regular measurement - not for a graph
        // This Collection size will be the number of parameters to measure
        public virtual IList<MeasureInstruction> measurementInstructions { get; set; }


    }
}