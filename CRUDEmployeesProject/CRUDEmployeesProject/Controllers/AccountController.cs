using CRUDEmployeesProject.Models;
using CRUDEmployeesProject.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRUDEmployeesProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userContext;

        public AccountController(UserService userContext)
        {
            _userContext = userContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _userContext.GetAll().FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if(user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            User user = _userContext.GetAll().FirstOrDefault(u => u.Email == model.Email);
            if (user == null)
            {
                _userContext.Create(new User { Email = model.Email, Password = model.Password });

                await Authenticate(user);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(key: "", errorMessage: "Incorrect values");
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, value: user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, value: user.Role?.Name)   
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, authenticationType: "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", controllerName: "Account");
        }
    }
}
