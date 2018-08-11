using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadScholar.BL
{
    [Table("AnswerByPhones")]
    public class AnswerByPhoneData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column(Order = 1)]
        public long ActivityId { get; set; }

        [Column(Order = 2)]
        public string Phone { get; set; }

        public string Answer { get; set; }
        public long RoomID { get; set; }
        public AnswerByPhoneData() { }

        public AnswerByPhoneData(long activityId, string phone, string answer, long RoomID)
        {
            this.ActivityId = activityId;
            this.Phone = phone;
            this.Answer = answer;
            this.RoomID = RoomID;
        }
    }
}
