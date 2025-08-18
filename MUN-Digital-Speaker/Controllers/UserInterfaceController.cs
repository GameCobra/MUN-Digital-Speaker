using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MUN_Digital_Speaker.Models;
using System.Security.Claims;

namespace MUN_Digital_Speaker.Controllers
{
    public class UserInterfaceController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction(actionName: nameof(Login), routeValues: new {loginID = 0, key = 123});
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public string Dashboard()
        {
            #pragma warning disable 8603, 8602
            return User.Identity.Name;
            #pragma warning restore 8603, 8602
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Login == 12345 && model.Key == 0)
            {
                var claims = new List<Claim> 
                {
                    new Claim(ClaimTypes.Name, model.Login.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                );

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login");
            return View(model);
        }
    }
}