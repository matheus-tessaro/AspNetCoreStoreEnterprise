using System.ComponentModel.DataAnnotations;

namespace SE.Identity.API.Models
{
    public class UserRegistryViewModel
    {
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
