using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class Experiment : Activity
    {

        public string ExperimentQuestion { get; set; }
        public virtual IList<Activity> activities { get; set; }
        public long ActiveExpID { get; set; }
    }
}