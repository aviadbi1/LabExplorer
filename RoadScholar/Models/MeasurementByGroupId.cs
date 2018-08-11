using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class MeasurementByGroupId
    {
        public long id { get; set; }
        public long expID { get; set; }
        public long RoomId { get; set; }
        public long GroupID { get; set; }
        // if measurementInstructions.Count == 1 -> regular measurement - not for a graph
        public virtual IList<OneMeasureByGroupId> measurements { get; set; }
        // This Collection size will be the number of parameters to measure
    }
}