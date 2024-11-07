using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Models
{
    public class LoginViewModel
    {
        // Common fields for both login and register actions
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        // Registration-only fields
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Username")]
        public string? Username { get; set; }
    }
}
