using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUN_Digital_Speaker.Data;
using MUN_Digital_Speaker.Models;
using System.Security.Claims;

namespace MUN_Digital_Speaker.Controllers
{
    public class UserInterfaceController : Controller
    {
        private readonly MUN_Digital_SpeakerContext _context;

        public UserInterfaceController(MUN_Digital_SpeakerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction(actionName: nameof(Dashboard)); //routeValues: new {loginID = 0, key = 123}
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SpeakRequest(SpeakRequest requested)
        {
            Delegation delegation = await _context.Delegation.FirstAsync(x => x.Login == requested.Login);
            delegation.RequestedToSpeak = true;
            if (requested.IsRevoking)
            {
                delegation.RequestedToSpeak = false;
            }
            DelegationsController delegationController = new DelegationsController(_context);

            try
            {
                _context.Update(delegation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!delegationController.DelegationExists(delegation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(actionName: nameof(Dashboard));
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            #pragma warning disable 8603, 8602
            Delegation? loggedInDelegation = await _context.Delegation.FirstAsync(x => x.Login.ToString() == User.Identity.Name);
            #pragma warning restore 8603, 8602

            ViewData["country"] = loggedInDelegation.Country;
            ViewData["login"] = loggedInDelegation.Login;
            ViewData["hasRequested"] = loggedInDelegation.RequestedToSpeak;
            return View();
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
    }
}