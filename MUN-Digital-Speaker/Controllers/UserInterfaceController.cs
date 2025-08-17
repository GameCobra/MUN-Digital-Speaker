using Microsoft.AspNetCore.Mvc;

namespace MUN_Digital_Speaker.Controllers
{
    public class UserInterfaceController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction(actionName: nameof(Login), routeValues: new {loginID = 0, key = 123});
        }

        [ValidateAntiForgeryToken]
        public IActionResult Login(int loginID, int key)
        {
            return View();
        }
    }
}
