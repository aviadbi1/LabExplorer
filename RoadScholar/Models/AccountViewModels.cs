using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Ajax.Utilities;

namespace RoadScholar.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resources))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resources.Resources))]
        public bool RememberMe { get; set; }
    }

    public class StudentLoginViewModel //not used
    {
        [Required]
        [Display(Name = "RoomNumber", ResourceType = typeof(Resources.Resources))]
        public long Room { get; set; }

        [Required]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Resources))]
        public string PhoneNumber { get; set; }

        public StudentLoginViewModel() { }
        public StudentLoginViewModel(long roomID, string phone)
        {
            Room = roomID;
            PhoneNumber = phone;
        }
    }

    public class TeacherRegisterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "FirstNameRequired")]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resources))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "LastNameRequired")]
        [Display(Name = "LastName", ResourceType = typeof(Resources.Resources))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "EmailRequired")]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "PhoneNumberRequired")]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Resources))]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "PasswordRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "PasswordMustBeAtLeast6")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resources))]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "PasswordsDontMatch")]
        public string ConfirmPassword { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "PasswordMustBeAtLeast6")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "PasswordsDontMatch")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
