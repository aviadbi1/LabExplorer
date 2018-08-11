using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class Teacher
    {
        public long id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "FirstNameRequired")]
        //[StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
        //              ErrorMessageResourceName = "FirstNameLong")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "LastNameRequired")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "EmailRequired")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "PhoneNumberRequired")]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public long RoomId { get; set; }

    }
}