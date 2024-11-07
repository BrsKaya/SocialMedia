using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Models{

    public class LoginViewModel{

        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string? Email {get;set;}

        [Required]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır", MinimumLength = 6)]
        [Display(Name = "Parola")]
        public string? Password {get;set;}
    }
    }

    

    
