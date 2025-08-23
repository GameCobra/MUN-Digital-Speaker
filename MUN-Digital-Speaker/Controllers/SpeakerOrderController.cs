using Microsoft.AspNetCore.Mvc;
using MUN_Digital_Speaker.Data;

namespace MUN_Digital_Speaker.Controllers
{
    public class SpeakerOrderController : Controller
    {
        private readonly MUN_Digital_SpeakerContext _context;

        public SpeakerOrderController(MUN_Digital_SpeakerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            /*
            var parsedSpeakerList = _context.Delegation.Where(x => x.RequestedToSpeak == true)
                                                       .Select(x => x.Country).ToList();

            if (parsedSpeakerList == null)
            {

            }
            */
            #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            List<string> speakingCountries = _context.Delegation.Where(x => x.RequestedToSpeak == true)
                               .OrderBy(x =>  x.TimesSpoken)
                               .ThenBy(x => x.Login)
                               .Select(x => x.Country).ToList();
            #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            //ViewData["displayList"] = speakingCountries;
            return View(speakingCountries);
        }
    }
}
