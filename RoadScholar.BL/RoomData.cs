using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    [Table("Rooms")]
    public class RoomData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public long CurrentActivityId { get; set; }

        public virtual IList<StudentData> LoggedInStudents { get; set; }


        public RoomData()
        {
            LoggedInStudents = new List<StudentData>();
        }

        public void updateCurrentActivityId(long actId)
        {
            CurrentActivityId = actId;
        }

        public void addStudent(StudentData studentData)
        {
            LoggedInStudents.Add(studentData);
        }
    }
}
