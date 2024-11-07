using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Data.EfCore;
using SocialMediaApp.Entity;
using SocialMediaApp.Models;

namespace SocialMediaApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly SocialMediaContext _context;

        public UsersController(SocialMediaContext context)
        {
            _context = context;
        }

        // GET: Login page
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login action
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Login logic here (e.g., checking credentials)
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Redirect to a secured page or dashboard after successful login
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            return View(model);
        }

        // POST: Register action
        [HttpPost]
        public IActionResult Register(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = model.Name,
                    UserName = model.Username,
                    Email = model.Email,
                    Password = model.Password, // Make sure to hash the password before saving in production
                    Image = "default.png" // Assign a default profile image
                };

                // Add the user to the database
                _context.Users.Add(user);
                _context.SaveChanges();

                // Redirect to login page after successful registration
                return RedirectToAction("Login");
            }
            return View("Login", model); // Return to the same view with validation errors
        }
    }
}
