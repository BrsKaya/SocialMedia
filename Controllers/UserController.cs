
using Microsoft.AspNetCore.Mvc;

namespace SocialMediaApp.Controllers{

    public class UsersController : Controller{

        public UsersController(){}

        public IActionResult Login(){
            return View();
        }
    }

}