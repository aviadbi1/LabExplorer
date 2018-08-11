using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    [Table("ActiveExperiments")]
    public class ActiveExperimentData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ActiveExpID { get; set; }
        public long ExpID { get; set; }
        public int NumberOfGroups { get; set; }
        public int MaxStudentPerGroup { get; set; }
        public int NumberCreatedGroups { get; set; }
        public ActiveExperimentData() { }

        public ActiveExperimentData(long ExpID, int NumberOfGroups, int MaxStudentPerGroup)
        {
            this.ExpID = ExpID;
            this.NumberOfGroups = NumberOfGroups;
            this.MaxStudentPerGroup = MaxStudentPerGroup;
            this.NumberCreatedGroups = 0;
        }

        public void IncrementCreatedGroupsCounter()
        {
            NumberCreatedGroups++;
        }
    }
}
