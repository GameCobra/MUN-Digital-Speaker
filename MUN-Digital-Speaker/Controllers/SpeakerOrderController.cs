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
        public IActionResult SpeakerListAsJSON(string? councel)
        {
            List<string> speakingCountries = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                if (councel == "GEN")
                {
                    speakingCountries = delegationsDBContext.Delegation
                                       .Where(x => x.RequestedToSpeakGEN == true)
                                       .Where(x => !_speakerControl.topSpeakerListGen.Contains(x.Country!))
                                       .OrderBy(x => x.TimesSpoken)
                                       .ThenBy(x => x.Login)
                                       .Select(x => x.Country)
                                       .ToList()!;
                }

                if (councel == "ECO")
                {
                    speakingCountries = delegationsDBContext.Delegation
                                       .Where(x => x.RequestedToSpeakECO == true)
                                       .Where(x => !_speakerControl.topSpeakerListGen.Contains(x.Country!))
                                       .OrderBy(x => x.TimesSpoken)
                                       .ThenBy(x => x.Login)
                                       .Select(x => x.Country)
                                       .ToList()!;
                }

                if (councel == "ENV")
                {
                    speakingCountries = delegationsDBContext.Delegation
                                       .Where(x => x.RequestedToSpeakENV == true)
                                       .Where(x => !_speakerControl.topSpeakerListGen.Contains(x.Country!))
                                       .OrderBy(x => x.TimesSpoken)
                                       .ThenBy(x => x.Login)
                                       .Select(x => x.Country)
                                       .ToList()!;
                }

                if (councel == "HE")
                {
                    speakingCountries = delegationsDBContext.Delegation
                                       .Where(x => x.RequestedToSpeakHE == true)
                                       .Where(x => !_speakerControl.topSpeakerListGen.Contains(x.Country!))
                                       .OrderBy(x => x.TimesSpoken)
                                       .ThenBy(x => x.Login)
                                       .Select(x => x.Country)
                                       .ToList()!;
                }
                
                if (councel == "SEC")
                {
                    speakingCountries = delegationsDBContext.Delegation
                                       .Where(x => x.RequestedToSpeakSEC == true)
                                       .Where(x => !_speakerControl.topSpeakerListGen.Contains(x.Country!))
                                       .OrderBy(x => x.TimesSpoken)
                                       .ThenBy(x => x.Login)
                                       .Select(x => x.Country)
                                       .ToList()!;
                }


                /*
                if (_speakerControl.topSpeakerListGen.Count() < 3)
                {
                    if (_speakerControl.topSpeakerListGen.Count() != 0)
                        _speakerControl.topSpeakerListGen.Add(speakingCountries[0]);
                }
                */
            }
            /*
            for (int i = 0; i < _speakerControl.topSpeakerListGen.Count(); i++)
            {
                speakingCountries.Insert(i, _speakerControl.topSpeakerListGen[i]);
            }
            */
            return Json(speakingCountries);
        }

        public IActionResult AmendmentListAsJSON(string? councel)
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

        public IActionResult ResolutionsAsJSON(string? councel)
        {
            if (_speakerControl.Resolutions != null)
                return Json(_speakerControl.Resolutions
                                            .Where(x => x.Councel == councel)
                                            .OrderBy(x => x.ID));
            else
                return Json(new List<string>());
        }

        public IActionResult ViewSpeakerOrder(string? councel)
        {
            ViewData["Councel"] = councel;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> SpeakerOrderDashboard(int? curRes)
        {
            if (!await delLookup.IsAdmin(User.Identity!.Name))
            {
                return Unauthorized();
            }
            ViewData["curRes"] = curRes;
            return View(_speakerControl.Resolutions);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Speak(string? councel)
        {
            if (!await delLookup.IsAdmin(User.Identity!.Name))
            {
                return Unauthorized();
            }

            if (councel == "GEN")
            {
                Delegation? topContry = delegationsDBContext.Delegation.Where(x => x.RequestedToSpeakGEN == true)
                                   .OrderBy(x => x.TimesSpoken)
                                   .ThenBy(x => x.Login)
                                   .FirstOrDefault();

                if (topContry != null)
                {
                    topContry.TimesSpoken += 1;
                    topContry.RequestedToSpeakGEN = false;

                    await delegationsDBContext.SaveChangesAsync();
                }
            }

            if (councel == "ECO")
            {
                Delegation? topContry = delegationsDBContext.Delegation.Where(x => x.RequestedToSpeakECO == true)
                                   .OrderBy(x => x.TimesSpoken)
                                   .ThenBy(x => x.Login)
                                   .FirstOrDefault();

                if (topContry != null)
                {
                    topContry.TimesSpoken += 1;
                    topContry.RequestedToSpeakECO = false;

                    await delegationsDBContext.SaveChangesAsync();
                }
            }

            if (councel == "ENV")
            {
                Delegation? topContry = delegationsDBContext.Delegation.Where(x => x.RequestedToSpeakENV == true)
                                   .OrderBy(x => x.TimesSpoken)
                                   .ThenBy(x => x.Login)
                                   .FirstOrDefault();

                if (topContry != null)
                {
                    topContry.TimesSpoken += 1;
                    topContry.RequestedToSpeakENV = false;

                    await delegationsDBContext.SaveChangesAsync();
                }
            }

            if (councel == "HE")
            {
                Delegation? topContry = delegationsDBContext.Delegation.Where(x => x.RequestedToSpeakHE == true)
                                   .OrderBy(x => x.TimesSpoken)
                                   .ThenBy(x => x.Login)
                                   .FirstOrDefault();

                if (topContry != null)
                {
                    topContry.TimesSpoken += 1;
                    topContry.RequestedToSpeakHE = false;

                    await delegationsDBContext.SaveChangesAsync();
                }
            }

            if (councel == "SEC")
            {
                Delegation? topContry = delegationsDBContext.Delegation.Where(x => x.RequestedToSpeakSEC == true)
                                   .OrderBy(x => x.TimesSpoken)
                                   .ThenBy(x => x.Login)
                                   .FirstOrDefault();

                if (topContry != null)
                {
                    topContry.TimesSpoken += 1;
                    topContry.RequestedToSpeakSEC = false;

                    await delegationsDBContext.SaveChangesAsync();
                }

            }



            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ClearSpeak(string? councel)
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }


            if (councel == "GEN")
            {
                var delegations = await delegationsDBContext.Delegation.Where(d => d.RequestedToSpeakGEN == true)
                                                           .ToListAsync();

                foreach (var delegation in delegations)
                {
                    delegation.RequestedToSpeakGEN = false;
                }
            }

            if (councel == "ECO")
            {
                var delegations = await delegationsDBContext.Delegation.Where(d => d.RequestedToSpeakECO == true)
                                                           .ToListAsync();

                foreach (var delegation in delegations)
                {
                    delegation.RequestedToSpeakECO = false;
                }
            }

            if (councel == "ENV")
            {
                var delegations = await delegationsDBContext.Delegation.Where(d => d.RequestedToSpeakENV == true)
                                                           .ToListAsync();

                foreach (var delegation in delegations)
                {
                    delegation.RequestedToSpeakENV = false;
                }
            }

            if (councel == "HE")
            {
                var delegations = await delegationsDBContext.Delegation.Where(d => d.RequestedToSpeakHE == true)
                                                           .ToListAsync();

                foreach (var delegation in delegations)
                {
                    delegation.RequestedToSpeakHE = false;
                }
            }

            if (councel == "SEC")
            {
                var delegations = await delegationsDBContext.Delegation.Where(d => d.RequestedToSpeakSEC == true)
                                                           .ToListAsync();

                foreach (var delegation in delegations)
                {
                    delegation.RequestedToSpeakSEC = false;
                }
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
            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder");
        }

        public IActionResult SetCurrentResolution(int resID)
        {
            _speakerControl.CurrentResolution = resID;
            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder", new { curRes = resID });
        }
    }
}
