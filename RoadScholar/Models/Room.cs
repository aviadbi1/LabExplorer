using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadScholar.Models
{
    public class Room
    {
        public long id { get; set; }

        public long CurrentActivityId { get; set; }

        public virtual IList<Student> LoggedInStudents {get; set;}
    }
}