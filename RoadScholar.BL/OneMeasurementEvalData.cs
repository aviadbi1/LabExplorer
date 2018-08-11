using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    // this class will contain the double value of the measurement
    public class OneMeasurementEvalData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public double value { get; set; }

        public OneMeasurementEvalData() { }
        public OneMeasurementEvalData(double value)
        {
            this.value = value;
        }
    }
}