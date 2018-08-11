using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RoadScholar.Models
{
    public class Group
    {
        public long GroupID { get; set; }
        public long ActiveExpID { get; set; }
        public virtual IList<string> StudentsList { get; set; }
        public int CurrentStepInExperiment { get; set; }
        public string GroupName { get; set; }
        public virtual IList<MeasurementByGroupId> measurementsList { get; set; }
        public string studentListAsString { get; set; }

    }
}