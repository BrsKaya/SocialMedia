using System.Security.Claims;
using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.Concreate.EfCore;
using SocialMediaApp.Models;
using SocialMediaApp.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Posts");
            }
            return View(new LoginActionViewModel());
        }

        /*
        [HttpPost]
        public async Task<IActionResult> Login1(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsRegistering) // Kullanıcı kayıt olmaya çalışıyor
                {
                    var userExists = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Email == model.Email);

                    if (userExists == null)
                    {
                        _userRepository.CreateUser(new User
                        {
                            UserName = model.UserName,
                            Name = model.Name,
                            Email = model.Email,
                            Password = model.Password,
                            Image = "pp.png"
                        });
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı veya email kullanımda.");
                    }
                }
                else // Kullanıcı giriş yapmaya çalışıyor
                {
                    var isUser = await _userRepository.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

                    if (isUser != null)
                    {
                        var userClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()),
                            new Claim(ClaimTypes.Name, isUser.UserName ?? ""),
                            new Claim(ClaimTypes.GivenName, isUser.Name ?? ""),
                            new Claim(ClaimTypes.UserData, isUser.Image ?? "")
                        };

                        if (isUser.Email == "baris@info.com")
                        {
                            userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                        }

                        var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var LoginProperties = new AuthenticationProperties { IsPersistent = true };

                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), LoginProperties);
                        return RedirectToAction("Index", "Posts");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı ve şifre hatalıdır.");
                    }
                }
            }
            return View(model);
        }
        */
        [HttpPost]
        public async Task<IActionResult> Login([Bind(Prefix = "Login")] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var isUser = await _userRepository.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

                if (isUser != null)
                {
                    var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()),
                    new Claim(ClaimTypes.Name, isUser.UserName ?? ""),
                    new Claim(ClaimTypes.GivenName, isUser.Name ?? ""),
                    new Claim(ClaimTypes.UserData, isUser.Image ?? "")
                };

                    if (isUser.Email == "baris@info.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var LoginProperties = new AuthenticationProperties { IsPersistent = true };

                    //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), LoginProperties);
                    return RedirectToAction("Index", "Posts");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı ve şifre hatalıdır.");
                }
            }

            return View(new LoginActionViewModel { Login = model });
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind(Prefix = "Register")] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {


                var userExists = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Email == model.Email);

                if (userExists == null)
                {
                    _userRepository.CreateUser(new User
                    {
                        UserName = model.UserName,
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        Image = "pp.png"
                    });
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya email kullanımda.");
                }
            }

            return View("Login", new LoginActionViewModel { Register = model });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }


        public IActionResult Profile(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            var user = _userRepository
                       .Users
                       .Include(x => x.Posts)
                       .Include(x => x.Comments)
                       .ThenInclude(x => x.Post)
                       .FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}