using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUN_Digital_Speaker.Data;
using MUN_Digital_Speaker.Models;

namespace MUN_Digital_Speaker.Controllers
{
    public class SpeakerOrderController : Controller
    {
        private readonly MUN_Digital_SpeakerContext _context;
        private readonly SpeakerListControlStates _speakerControl;


        public SpeakerOrderController(MUN_Digital_SpeakerContext context, SpeakerListControlStates speakerControl)
        {
            _context = context;
            _speakerControl = speakerControl;
        }
        public IActionResult SpeakerListAsJSON()
        {
            #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            List<string> speakingCountries = _context.Delegation.Where(x => x.RequestedToSpeak == true)
                               .OrderBy(x => x.TimesSpoken)
                               .ThenBy(x => x.Login)
                               .Select(x => x.Country).ToList();
            #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            //ViewData["displayList"] = speakingCountries;
            return Json(speakingCountries);
        }



        public IActionResult Index()
        {
            #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            List<string> speakingCountries = _context.Delegation.Where(x => x.RequestedToSpeak == true)
                               .OrderBy(x =>  x.TimesSpoken)
                               .ThenBy(x => x.Login)
                               .Select(x => x.Country).ToList();
            #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            //ViewData["displayList"] = speakingCountries;
            return View(speakingCountries);
        }

        public IActionResult SpeakerOrderDashboard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Speak()
        {
            #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            Delegation topContry = _context.Delegation.Where(x => x.RequestedToSpeak == true)
                               .OrderBy(x => x.TimesSpoken)
                               .ThenBy(x => x.Login)
                               .First();
            #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            topContry.TimesSpoken += 1;
            topContry.RequestedToSpeak = false;

            await _context.SaveChangesAsync();

            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder");
        }

        [HttpPost]
        public async Task<IActionResult> ClearSpeak()
        {
            var delegations = await _context.Delegation.Where(d => d.RequestedToSpeak == true)
                                                       .ToListAsync();

            foreach (var delegation in delegations)
            {
                delegation.RequestedToSpeak = false;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder");
        }

        [HttpPost]
        public IActionResult Lock()
        {
            _speakerControl.allowSpeakRequests = false;
            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder");
        }
    }
}
