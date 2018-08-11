using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    [Table("Students")]
    public class StudentData
    {
        [Key]
        [Column(Order = 1)]
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Key]
        [Column(Order = 2)]
        public long RoomId { get; set; }

        public virtual long GroupID { get; set; }

        //[ForeignKey("GroupID")]
        //public virtual GroupData groupData { get; set; }

        public StudentData() { }

        public StudentData(string PhoneNumber, string FirstName, string LastName, string Email)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.GroupID = 0;
        }

        public bool isAvailable()
        {
            return GroupID == 0;
        }
    }
}
