using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUN_Digital_Speaker.Data;
using MUN_Digital_Speaker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace MUN_Digital_Speaker.Controllers
{
    public class SpeakerOrderController : Controller
    {
        private readonly MUN_Digital_SpeakerContext delegationsDBContext;
        private readonly SpeakerListControlStates _speakerControl;
        DelegationLookup delLookup;



        public SpeakerOrderController(MUN_Digital_SpeakerContext context, SpeakerListControlStates speakerControl, DelegationLookup delLook)
        {
            delegationsDBContext = context;
            _speakerControl = speakerControl;
            delLookup = delLook;

        }
        public IActionResult SpeakerListAsJSON()
        {
            #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            List<string> speakingCountries = delegationsDBContext.Delegation.Where(x => x.RequestedToSpeak == true)
                               .OrderBy(x => x.TimesSpoken)
                               .ThenBy(x => x.Login)
                               .Select(x => x.Country).ToList();
            #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            //ViewData["displayList"] = speakingCountries;
            return Json(speakingCountries);
        }

        public IActionResult AmendmentListAsJSON()
        {
            int currentResoluton = _speakerControl.CurrentResolution;

            #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.

            List<String> speakingCountries = delegationsDBContext.Delegation
                               .OrderBy(x => x.AmendmentPoints)
                               .ThenBy(x => x.Login)
                               //.Where(x => x.amendments != null && x.amendments.Count() > 0)
                               .Where(x => x.amendments.Any(y => y.ResolutionID == currentResoluton))
                               .Select(x => x.Country).ToList();
        #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            //ViewData["displayList"] = speakingCountries;
            return Json(speakingCountries);
        }

        public IActionResult ResolutionsAsJSON()
        {
            if (_speakerControl.Resolutions != null)
                return Json(_speakerControl.Resolutions.OrderBy(x => x.ID));
            else
                return Json(new List<string>());
        }



        public IActionResult Index() 
        {
            //Will be a home page for generating the screens
            return View();
        }

        public IActionResult ViewSpeakerOrder()
        {

            return View();
        }

        public IActionResult ViewAmendmentOrder()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> SpeakerOrderDashboard()
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            return View(_speakerControl.Resolutions);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Speak()
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }


            #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            Delegation? topContry = delegationsDBContext.Delegation.Where(x => x.RequestedToSpeak == true)
                               .OrderBy(x => x.TimesSpoken)
                               .ThenBy(x => x.Login)
                               .FirstOrDefault();
            #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.


            if (topContry != null)
            {
                topContry.TimesSpoken += 1;
                topContry.RequestedToSpeak = false;

                await delegationsDBContext.SaveChangesAsync();
            }

            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ClearSpeak()
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            var delegations = await delegationsDBContext.Delegation.Where(d => d.RequestedToSpeak == true)
                                                       .ToListAsync();

            foreach (var delegation in delegations)
            {
                delegation.RequestedToSpeak = false;
            }

            await delegationsDBContext.SaveChangesAsync();

            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Lock()
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            _speakerControl.allowSpeakRequests = false;
            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Unlock()
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            _speakerControl.allowSpeakRequests = true;
            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder");
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangeResolution(string ResID, string state)
        {
            Resolution? res = _speakerControl.Resolutions.FirstOrDefault(x => x.ID.ToString() == ResID);
            if (res != null)
                res.State = state;
            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrderDashboard");
        }
    }
}
