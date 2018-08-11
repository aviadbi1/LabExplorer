using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class MeasurementByGroupIdData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public long expID { get; set; }
        public long RoomId { get; set; }
        public long GroupID { get; set; }
        public long CurrentActivityIndex { get; set; }
        // if measurementInstructions.Count == 1 -> regular measurement - not for a graph
        public virtual IList<OneMeasureByGroupIdData> measurements { get; set; }
        // This Collection size will be the number of parameters to measure

        public MeasurementByGroupIdData() { }

        public MeasurementByGroupIdData(long expID, long RoomId, long GroupID, long CurrentActivityIndex)
        {
            this.expID = expID;
            this.RoomId = RoomId;
            this.GroupID = GroupID;
            this.CurrentActivityIndex = CurrentActivityIndex;
            this.measurements = new List<OneMeasureByGroupIdData>();
        }

        public void addOneMeasure(OneMeasureByGroupIdData oneM)
        {
            measurements.Add(oneM);
        }
    }
}
