using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    [Table("Teachers")]
    public class TeacherData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public long RoomId { get; set; }

        public TeacherData() { }

        public TeacherData(string FirstName, string LastName, string Email, string PhoneNumber, string Password)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Password = Password;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;

            RoomData room = new RoomData();
            RoadScholarContext rsContext = new RoadScholarContext();
            rsContext.addRoom(room);

            RoomId = room.id;
        }

        private bool StudentNotExist(string phoneOfStudent)
        {
            RoadScholarContext rsContext = new RoadScholarContext();
            StudentData studentData = rsContext.getStudent(phoneOfStudent, RoomId);
            if (studentData == null)
            {
                return true;
            }
            return false;
        }

        public bool registerStudent(StudentData studentData)
        {
            if (StudentNotExist(studentData.PhoneNumber))
            {
                RoadScholarContext rsContext = new RoadScholarContext();
                rsContext.addStudent(studentData);
                return true;
            }
            return false;
        }


        public string registerStudentsFromList(List<StudentData> studentsData)
        {
            string existingStudents = "";
            foreach (StudentData stData in studentsData)
            {
                if (!registerStudent(stData))
                {
                    //know the teacher that stData exist
                    existingStudents += stData.FirstName + " " + stData.LastName + ", ";
                }
            }
            return existingStudents;
        }
    }
}
