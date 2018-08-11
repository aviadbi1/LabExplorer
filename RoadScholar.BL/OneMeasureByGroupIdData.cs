using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    // This class is one 'row' of measurements from the student
    public class OneMeasureByGroupIdData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public long MeasurementByGroupIDid { get; set; }

        // This collection size is the number of the parameters
        public virtual IList<OneMeasurementEvalData> measurements { get; set; }

        public OneMeasureByGroupIdData() { }
        public OneMeasureByGroupIdData(long MeasurementByGroupIDid)
        {
            this.MeasurementByGroupIDid = MeasurementByGroupIDid;
            this.measurements = new List<OneMeasurementEvalData>();
        }

        public void addParameterMeasurement(double m)
        {
            measurements.Add(new OneMeasurementEvalData(m));
        }

        public double getFirstInMeasurements()
        {
            return measurements.ElementAt(0).value;
        }
    }
}
