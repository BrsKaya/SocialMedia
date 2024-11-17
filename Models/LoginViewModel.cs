using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Models
{
    public class LoginViewModel{
        // Login için gerekli alanlar
        [Required(ErrorMessage = "Eposta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir eposta adresi giriniz")]
        [Display(Name = "Eposta")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Parola gereklidir")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır", MinimumLength = 6)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }
    }

    public class RegisterViewModel{

        // Login için gerekli alanlar
        [Required(ErrorMessage = "Eposta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir eposta adresi giriniz")]
        [Display(Name = "Eposta")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Parola gereklidir")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır", MinimumLength = 6)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }

        // Register işlemi için ek alanlar
        public bool IsRegistering { get; set; } // Kullanıcının kayıt olup olmadığını belirlemek için

        [Display(Name = "Kullanıcı Adı")]
        [StringLength(20, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string? UserName { get; set; }

        [Display(Name = "Ad Soyad")]
        [StringLength(50, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string? Name { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parolanız eşleşmedi.")]
        [Display(Name = "Parola Tekrar")]
        public string? ConfirmPassword { get; set; }
    }

    public class LoginActionViewModel{
        public LoginViewModel Login{get;set;}

        public RegisterViewModel Register{get;set;}
    }

    public class LoginViewModel1
    {
        // Login için gerekli alanlar
        [Required(ErrorMessage = "Eposta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir eposta adresi giriniz")]
        [Display(Name = "Eposta")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Parola gereklidir")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır", MinimumLength = 6)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }

        // Register işlemi için ek alanlar
        public bool IsRegistering { get; set; } // Kullanıcının kayıt olup olmadığını belirlemek için

        [Display(Name = "Kullanıcı Adı")]
        [StringLength(20, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string? UserName { get; set; }

        [Display(Name = "Ad Soyad")]
        [StringLength(50, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string? Name { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parolanız eşleşmedi.")]
        [Display(Name = "Parola Tekrar")]
        public string? ConfirmPassword { get; set; }
    }
}