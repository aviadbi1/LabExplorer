using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class AnswerByPhone
    {
        public long Id { get; set; }

        [Required]
        public long ActivityId { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Answer { get; set; }

        public long RoomID { get; set; }
    }
}