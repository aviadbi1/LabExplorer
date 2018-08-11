using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class MeasureInstructionData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public string MeasurementInstruction { get; set; }

        //public long MeasurementID { get; set; }

        public MeasureInstructionData() { }
        public MeasureInstructionData(long MeasurementID, string MeasurementInstruction)
        {
            //this.MeasurementID = MeasurementID;
            this.MeasurementInstruction = MeasurementInstruction;
        }

        public MeasureInstructionData(MeasureInstructionData instruction)
        {
            this.MeasurementInstruction = instruction.MeasurementInstruction;
        }
    }
}
