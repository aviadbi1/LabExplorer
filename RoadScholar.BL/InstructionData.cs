using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class InstructionData : ActivityData
    {
        public string Command { get; set; }

        public InstructionData()
        {
            this.Type = "Instruction";
        }

        public InstructionData(string Command, string ActivityName) : base(ActivityName)
        {
            this.Command = Command;
            this.Type = "Instruction";
        }

        public InstructionData(InstructionData iData) : base(iData)
        {
            this.Command = iData.Command;
            this.Type = "Instruction";
        }
    }
}
