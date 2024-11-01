using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Data.EfCore;
using SocialMediaApp.Entity;
using SocialMediaApp.Models;

namespace SocialMediaApp.Controllers {
    public class UsersController : Controller {
        private readonly SocialMediaContext _context;

        public UsersController(SocialMediaContext context) {
            _context = context;
        }

        public IActionResult Login() {
            return View();
        }

        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public IActionResult Register(LoginViewModel model) {
            if (ModelState.IsValid) {
                var user = new User {
                    UserName = model.Email, // Kullanıcı adını e-posta olarak ayarlamak isteyebilirsiniz.
                    Image = "default.png" // Varsayılan bir profil resmi ekleyebilirsiniz.
                };

                // Kullanıcıyı veritabanına ekleme
                _context.Users.Add(user);
                _context.SaveChanges();

                // Başarılı kayıt sonrası yönlendirme
                return RedirectToAction("Login");
            }
            return View(model);
        }
    }
}
