using System.ComponentModel.DataAnnotations;
using SocialMediaApp.Entity;

namespace SocialMediaApp.Models{

    public class PostCreateViewModel{

        public int PostId {get;set;}
        public bool IsActive {get;set;}

        [Required]
        [StringLength(300)]
        [Display(Name = "İçerik")]
        public string? Content {get;set;}

        public string? Url {get;set;}
        
    }
}