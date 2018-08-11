using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class Student
    {
        [Required]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Resources))]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resources))]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(Resources.Resources))]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "RoomNumber", ResourceType = typeof(Resources.Resources))]
        public long RoomId { get; set; }

    }
}