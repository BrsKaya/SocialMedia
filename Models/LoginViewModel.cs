using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Models{

    public class LoginViewModel{

        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string? Email {get;set;}

        [Required]
        [DataType(DataType.Password)]
        
        [Display(Name = "Parola")]
        public string? Password {get;set;}
    }
}