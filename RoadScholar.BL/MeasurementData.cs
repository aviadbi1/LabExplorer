using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class MeasurementData : ActivityData
    {
        [Required]
        public long NumOfMeasures { get; set; }
        [Required]
        public long DifferenceBetweenMeasures { get; set; }
        [Required]
        public long NumOfParametersToMeasure { get; set; }
        [Required]
        public long WindowOpenTimeSeconds { get; set; }
        // if measurementInstructions.Count == 1 -> regular measurement - not for a graph
        public virtual IList<MeasureInstructionData> measurementInstructions { get; set; }

        public MeasurementData() : base(false)
        {
            this.measurementInstructions = new List<MeasureInstructionData>();
            this.Type = "Measurement";
        }

        public MeasurementData(long NumOfMeasures, long DifferenceBetweenMeasures, long NumOfParametersToMeasure, long WindowOpenTimeSeconds, string ActivityName) : base(ActivityName)
        {
            this.NumOfMeasures = NumOfMeasures;
            this.DifferenceBetweenMeasures = DifferenceBetweenMeasures;
            this.NumOfParametersToMeasure = NumOfParametersToMeasure;
            this.WindowOpenTimeSeconds = WindowOpenTimeSeconds;
            this.measurementInstructions = new List<MeasureInstructionData>();
            this.Type = "Measurement";
        }

        public MeasurementData(MeasurementData measurement) : base(measurement)
        {
            this.NumOfMeasures = measurement.NumOfMeasures;
            this.DifferenceBetweenMeasures = measurement.DifferenceBetweenMeasures;
            this.NumOfParametersToMeasure = measurement.NumOfParametersToMeasure;
            this.WindowOpenTimeSeconds = measurement.WindowOpenTimeSeconds;
            this.measurementInstructions = new List<MeasureInstructionData>();
            foreach(MeasureInstructionData instruction in measurement.measurementInstructions)
            {
                this.measurementInstructions.Add(new MeasureInstructionData(instruction));
            }
            this.Type = "Measurement";
        }

        public void AddInstruction(string instruction)
        {
            MeasureInstructionData mid = new MeasureInstructionData(id, instruction);
            measurementInstructions.Add(mid);
        }
    }
}
