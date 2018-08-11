using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace RoadScholar.Models
{
    public class TrueFalseQuestionResponse
    {
        [Required]
        public bool Answer { get; set; }

        public string PhoneNumber { get; set; }

        public long RoomId { get; set; }

        public long ActivityId { get; set; }

        public long ExpId { get; set; }
        public int CurrActivityIndex { get; set; }
    }

    public class ShortAnswerQuestionResponse
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                    ErrorMessageResourceName = "AnswerRequired")]
        public string Answer { get; set; }

        public string PhoneNumber { get; set; }

        public long RoomId { get; set; }

        public long ActivityId { get; set; }

        public long ExpId { get; set; }
        public int CurrActivityIndex { get; set; }
    }

    public class AmericanQuestionResponse
    {
        [Required]
        public int Answer { get; set; }

        public string PhoneNumber { get; set; }

        public long RoomId { get; set; }

        public long ActivityId { get; set; }
        public long ExpId { get; set; }
        public int CurrActivityIndex { get; set; }
    }

    public class InstructionResponse
    {
        public string PhoneNumber { get; set; }
        public long RoomId { get; set; }
        public long ActivityId { get; set; }
        public long ExpId { get; set; }
        public int CurrActivityIndex { get; set; }
    }

    public class ImageResponse
    {
        public string PhoneNumber { get; set; }
        public long RoomId { get; set; }
        public long ActivityId { get; set; }
        public long ExpId { get; set; }
        public int CurrActivityIndex { get; set; }
    }

    public class VideoResponse
    {
        public string PhoneNumber { get; set; }
        public long RoomId { get; set; }
        public long ActivityId { get; set; }
        public long ExpId { get; set; }
        public int CurrActivityIndex { get; set; }
    }
}