using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MUN_Digital_Speaker.Data;
using MUN_Digital_Speaker.Models;
using System.Security.Claims;

namespace MUN_Digital_Speaker.Controllers
{
    public class UserAccountManagerController : Controller
    {
        private readonly MUN_Digital_SpeakerContext _context;
        private readonly SpeakerListControlStates _speakerControl;


        public UserAccountManagerController(MUN_Digital_SpeakerContext context, SpeakerListControlStates speakerControl)
        {
            _context = context;
            _speakerControl = speakerControl;
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
            if (!ModelState.IsValid)
                return View(model);

            bool isValadeDelegation = _context.Delegation.Any(x => x.Login == model.Login && x.Key == model.Key);

            if (isValadeDelegation)
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
