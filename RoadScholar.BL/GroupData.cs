using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    public class GroupData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GroupID { get; set; }
        public long ActiveExpID { get; set; }
        public virtual IList<StudentData> Students { get; set; }
        public int Progress { get; set; }
        public virtual IList<MeasurementByGroupIdData> measurementsList { get; set; }
        public string studentListAsString { get; set; }

        public GroupData()
        {
            this.Students = new List<StudentData>();
            this.Progress = 0;
        }

        public GroupData(long ActiveExpID, List<StudentData> StudentsList)
        {
            this.ActiveExpID = ActiveExpID;
            this.Students = StudentsList;
            this.Progress = 0;
            measurementsList = new List<MeasurementByGroupIdData>();
        }

        public void AddMeasurement(MeasurementByGroupIdData md)
        {
            measurementsList.Add(md);
        }

        public MeasurementByGroupIdData getLastInMeasurements()
        {
            if (measurementsList.Count == 0)
            {
                return null;
            }
            return measurementsList.ElementAt(measurementsList.Count - 1);
        }

        public StudentData getfirstInStudents()
        {
            if (Students != null && Students.Count > 0)
            {
                return Students.ElementAt(0);
            }
            return null;
        }

        public void SaveAllStudentInGroupAsList()
        {
            studentListAsString = "";
            int index = 0;

            foreach (StudentData sd in Students)
            {
                if (index == 0)
                    studentListAsString += " " + sd.FirstName + " " + sd.LastName;
                else
                    studentListAsString += ", " + sd.FirstName + " " + sd.LastName;
                index++;
            }
        }
    }
}
