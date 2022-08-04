using SE.WebApp.MVC.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SE.WebApp.MVC.Models
{
    public class UserRegistryViewModel
    {
        [Required(ErrorMessage = "{0} is a required field")]
        [DisplayName("Full Name")]
        public string Name { get; set; }

        [SSN]
        [Required(ErrorMessage = "{0} is a required field")]
        [DisplayName("Social Security Number")]
        public string SocialSecurityNumber { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "Invalid {0}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} lenght must have a minumum of {2} and maximum of{1} charactes", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords must match")]
        public string PasswordConfirmation { get; set; }
    }
}
