using Microsoft.AspNetCore.Mvc;

namespace MUN_Digital_Speaker.Controllers
{
    public class SpeakerOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
