using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using MUN_Digital_Speaker.Data;
using MUN_Digital_Speaker.Models;

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
            if (councel == "GEN")
            {
                int j = _speakerControl.topSpeakerListGEN.Count() - 1;
                while (j >= 0)
                {
                    if (delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.topSpeakerListGEN[j]).RequestedToSpeakGEN == false)
                    {
                        _speakerControl.topSpeakerListGEN.RemoveAt(j);
                    }
                    j--;
                }

                for (int i = 0; i < 4; i++)
                {

                    speakingCountries = delegationsDBContext.Delegation
                                    .Where(x => x.RequestedToSpeakGEN == true)
                                    .Where(x => !_speakerControl.topSpeakerListGEN.Contains(x.Country!))
                                    .OrderBy(x => x.TimesSpoken)
                                    .ThenBy(x => x.Login)
                                    .Select(x => x.Country)
                                    .ToList()!;

                    if (_speakerControl.topSpeakerListGEN.Count() < 3)
                    {
                        if (speakingCountries.Count() != 0)
                            _speakerControl.topSpeakerListGEN.Add(speakingCountries[0]);
                    }
                }

                for (int i = 0; i < _speakerControl.topSpeakerListGEN.Count(); i++)
                {
                    speakingCountries.Insert(i, _speakerControl.topSpeakerListGEN[i]);
                }
            }

            if (councel == "ECO")
            {
                int j = _speakerControl.topSpeakerListECO.Count() - 1;
                while (j >= 0)
                {
                    if (delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.topSpeakerListECO[j]).RequestedToSpeakECO == false)
                    {
                        _speakerControl.topSpeakerListECO.RemoveAt(j);
                    }
                    j--;
                }

                for (int i = 0; i < 4; i++)
                {
                    speakingCountries = delegationsDBContext.Delegation
                                    .Where(x => x.RequestedToSpeakECO == true)
                                    .Where(x => !_speakerControl.topSpeakerListECO.Contains(x.Country!))
                                    .OrderBy(x => x.TimesSpoken)
                                    .ThenBy(x => x.Login)
                                    .Select(x => x.Country)
                                    .ToList()!;

                    if (_speakerControl.topSpeakerListECO.Count() < 3)
                    {
                        if (speakingCountries.Count() != 0)
                            _speakerControl.topSpeakerListECO.Add(speakingCountries[0]);
                    }
                }

                for (int i = 0; i < _speakerControl.topSpeakerListECO.Count(); i++)
                {
                    speakingCountries.Insert(i, _speakerControl.topSpeakerListECO[i]);
                }
            }

            if (councel == "ENV")
            {
                int j = _speakerControl.topSpeakerListENV.Count() - 1;
                while (j >= 0)
                {
                    if (delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.topSpeakerListENV[j]).RequestedToSpeakENV == false)
                    {
                        _speakerControl.topSpeakerListENV.RemoveAt(j);
                    }
                    j--;
                }

                for (int i = 0; i < 4; i++)
                {
                    speakingCountries = delegationsDBContext.Delegation
                                    .Where(x => x.RequestedToSpeakENV == true)
                                    .Where(x => !_speakerControl.topSpeakerListENV.Contains(x.Country!))
                                    .OrderBy(x => x.TimesSpoken)
                                    .ThenBy(x => x.Login)
                                    .Select(x => x.Country)
                                    .ToList()!;

                    if (_speakerControl.topSpeakerListENV.Count() < 3)
                    {
                        if (speakingCountries.Count() != 0)
                            _speakerControl.topSpeakerListENV.Add(speakingCountries[0]);
                    }
                }

                for (int i = 0; i < _speakerControl.topSpeakerListENV.Count(); i++)
                {
                    speakingCountries.Insert(i, _speakerControl.topSpeakerListENV[i]);
                }
            }

            if (councel == "HE")
            {
                int j = _speakerControl.topSpeakerListHE.Count() - 1;
                while (j >= 0)
                {
                    if (delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.topSpeakerListHE[j]).RequestedToSpeakHE == false)
                    {
                        _speakerControl.topSpeakerListHE.RemoveAt(j);
                    }
                    j--;
                }

                for (int i = 0; i < 4; i++)
                {
                    speakingCountries = delegationsDBContext.Delegation
                                    .Where(x => x.RequestedToSpeakHE == true)
                                    .Where(x => !_speakerControl.topSpeakerListHE.Contains(x.Country!))
                                    .OrderBy(x => x.TimesSpoken)
                                    .ThenBy(x => x.Login)
                                    .Select(x => x.Country)
                                    .ToList()!;

                    if (_speakerControl.topSpeakerListHE.Count() < 3)
                    {
                        if (speakingCountries.Count() != 0)
                            _speakerControl.topSpeakerListHE.Add(speakingCountries[0]);
                    }
                }

                for (int i = 0; i < _speakerControl.topSpeakerListHE.Count(); i++)
                {
                    speakingCountries.Insert(i, _speakerControl.topSpeakerListHE[i]);
                }
            }
                
            if (councel == "SEC")
            {
                int j = _speakerControl.topSpeakerListSEC.Count() - 1;
                while (j >= 0)
                {
                    if (delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.topSpeakerListSEC[j]).RequestedToSpeakSEC == false)
                    {
                        _speakerControl.topSpeakerListSEC.RemoveAt(j);
                    }
                    j--;
                }

                for (int i = 0; i < 4; i++)
                {
                    speakingCountries = delegationsDBContext.Delegation
                                    .Where(x => x.RequestedToSpeakSEC == true)
                                    .Where(x => !_speakerControl.topSpeakerListSEC.Contains(x.Country!))
                                    .OrderBy(x => x.TimesSpoken)
                                    .ThenBy(x => x.Login)
                                    .Select(x => x.Country)
                                    .ToList()!;

                    if (_speakerControl.topSpeakerListSEC.Count() < 3)
                    {
                        if (speakingCountries.Count() != 0)
                            _speakerControl.topSpeakerListSEC.Add(speakingCountries[0]);
                    }
                }

                for (int i = 0; i < _speakerControl.topSpeakerListSEC.Count(); i++)
                {
                    speakingCountries.Insert(i, _speakerControl.topSpeakerListSEC[i]);
                }
            }

            return Json(speakingCountries);
        }

        public IActionResult AmendmentListAsJSON(string councel)
        {
            return Json(GetAmendmentList(councel).Select(x => x.Country).ToList()!);
        }

        public List<Delegation> GetAmendmentList(string councel)
        {
            int currentResolution = -1;
            List<Delegation> speakingCountries = new List<Delegation>();

            if (councel == "GEN")
            {
                speakingCountries = delegationsDBContext.Delegation
                   .OrderBy(x => x.AmendmentPoints)
                   .ThenBy(x => x.Login)
                   .Where(x => x.amendments != null && x.amendments!.Count() > 0)
                   .Where(x => x.amendments!.Any(y => y.ResolutionID == currentResolution))
                   .Where(x => x.Country != _speakerControl.TopAmendmentGEN).ToList();

                currentResolution = _speakerControl.CurrentResolutionGEN;
                if (_speakerControl.TopAmendmentGEN == "" && speakingCountries.Count() > 0)
                {
                    _speakerControl.TopAmendmentGEN = speakingCountries[0].Country!;
                }

                speakingCountries = delegationsDBContext.Delegation
                   .OrderBy(x => x.AmendmentPoints)
                   .ThenBy(x => x.Login)
                   .Where(x => x.amendments != null && x.amendments!.Count() > 0)
                   .Where(x => x.amendments!.Any(y => y.ResolutionID == currentResolution))
                   .Where(x => x.Country != _speakerControl.TopAmendmentGEN).ToList();

                if (_speakerControl.TopAmendmentGEN != "")
                {
                    speakingCountries.Insert(0, delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.TopAmendmentGEN));
                }
            }


            if (councel == "ECO")
            {
                speakingCountries = delegationsDBContext.Delegation
                   .OrderBy(x => x.AmendmentPoints)
                   .ThenBy(x => x.Login)
                   .Where(x => x.amendments != null && x.amendments!.Count() > 0)
                   .Where(x => x.amendments!.Any(y => y.ResolutionID == currentResolution))
                   .Where(x => x.Country != _speakerControl.TopAmendmentECO).ToList();

                currentResolution = _speakerControl.CurrentResolutionECO;
                if (_speakerControl.TopAmendmentECO == "" && speakingCountries.Count() > 0)
                {
                    _speakerControl.TopAmendmentECO = speakingCountries[0].Country!;
                }

                speakingCountries = delegationsDBContext.Delegation
                   .OrderBy(x => x.AmendmentPoints)
                   .ThenBy(x => x.Login)
                   .Where(x => x.amendments != null && x.amendments!.Count() > 0)
                   .Where(x => x.amendments!.Any(y => y.ResolutionID == currentResolution))
                   .Where(x => x.Country != _speakerControl.TopAmendmentECO).ToList();

                if (_speakerControl.TopAmendmentECO != "")
                {
                    speakingCountries.Insert(0, delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.TopAmendmentECO));
                }
            }

            if (councel == "ENV")
            {
                speakingCountries = delegationsDBContext.Delegation
                   .OrderBy(x => x.AmendmentPoints)
                   .ThenBy(x => x.Login)
                   .Where(x => x.amendments != null && x.amendments!.Count() > 0)
                   .Where(x => x.amendments!.Any(y => y.ResolutionID == currentResolution))
                   .Where(x => x.Country != _speakerControl.TopAmendmentENV).ToList();

                currentResolution = _speakerControl.CurrentResolutionENV;
                if (_speakerControl.TopAmendmentENV == "" && speakingCountries.Count() > 0)
                {
                    _speakerControl.TopAmendmentENV = speakingCountries[0].Country!;
                }

                speakingCountries = delegationsDBContext.Delegation
                   .OrderBy(x => x.AmendmentPoints)
                   .ThenBy(x => x.Login)
                   .Where(x => x.amendments != null && x.amendments!.Count() > 0)
                   .Where(x => x.amendments!.Any(y => y.ResolutionID == currentResolution))
                   .Where(x => x.Country != _speakerControl.TopAmendmentENV).ToList();

                if (_speakerControl.TopAmendmentENV != "")
                {
                    speakingCountries.Insert(0, delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.TopAmendmentENV));
                }
            }

            if (councel == "HE")
            {
                speakingCountries = delegationsDBContext.Delegation
                   .OrderBy(x => x.AmendmentPoints)
                   .ThenBy(x => x.Login)
                   .Where(x => x.amendments != null && x.amendments!.Count() > 0)
                   .Where(x => x.amendments!.Any(y => y.ResolutionID == currentResolution))
                   .Where(x => x.Country != _speakerControl.TopAmendmentHE).ToList();

                currentResolution = _speakerControl.CurrentResolutionHE;
                if (_speakerControl.TopAmendmentHE == "" && speakingCountries.Count() > 0)
                {
                    _speakerControl.TopAmendmentHE = speakingCountries[0].Country!;
                }

                speakingCountries = delegationsDBContext.Delegation
                   .OrderBy(x => x.AmendmentPoints)
                   .ThenBy(x => x.Login)
                   .Where(x => x.amendments != null && x.amendments!.Count() > 0)
                   .Where(x => x.amendments!.Any(y => y.ResolutionID == currentResolution))
                   .Where(x => x.Country != _speakerControl.TopAmendmentHE).ToList();

                if (_speakerControl.TopAmendmentHE != "")
                {
                    speakingCountries.Insert(0, delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.TopAmendmentHE));
                }
            }

            if (councel == "SEC")
            {
                speakingCountries = delegationsDBContext.Delegation
                   .OrderBy(x => x.AmendmentPoints)
                   .ThenBy(x => x.Login)
                   .Where(x => x.amendments != null && x.amendments!.Count() > 0)
                   .Where(x => x.amendments!.Any(y => y.ResolutionID == currentResolution))
                   .Where(x => x.Country != _speakerControl.TopAmendmentSEC).ToList();

                currentResolution = _speakerControl.CurrentResolutionSEC;
                if (_speakerControl.TopAmendmentSEC == "" && speakingCountries.Count() > 0)
                {
                    _speakerControl.TopAmendmentSEC = speakingCountries[0].Country!;
                }

                speakingCountries = delegationsDBContext.Delegation
                   .OrderBy(x => x.AmendmentPoints)
                   .ThenBy(x => x.Login)
                   .Where(x => x.amendments != null && x.amendments!.Count() > 0)
                   .Where(x => x.amendments!.Any(y => y.ResolutionID == currentResolution))
                   .Where(x => x.Country != _speakerControl.TopAmendmentSEC).ToList();

                if (_speakerControl.TopAmendmentSEC != "")
                {
                    speakingCountries.Insert(0, delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.TopAmendmentSEC));
                }
            }

            //ViewData["displayList"] = speakingCountries;
            return speakingCountries;
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

        public string GetTopAmendmentText(string councel)
        {
            List<Delegation> am = GetAmendmentList(councel);
            if (am != null && am.Count() >= 1)
            {
                if (councel == "GEN")
                {
                    string topAmmendmentText = am![0].amendments.FirstOrDefault(x => x.ResolutionID == _speakerControl!.CurrentResolutionGEN!).Change;
                    string clasue = am![0].amendments.FirstOrDefault(x => x.ResolutionID == _speakerControl!.CurrentResolutionGEN!).ClauseNumber;
                    return "(" + clasue + ") " + topAmmendmentText;
                }
                if (councel == "ECO")
                {
                    string topAmmendmentText = am![0].amendments.FirstOrDefault(x => x.ResolutionID == _speakerControl!.CurrentResolutionECO!).Change;
                    string clasue = am![0].amendments.FirstOrDefault(x => x.ResolutionID == _speakerControl!.CurrentResolutionECO!).ClauseNumber;
                    return "(" + clasue + ") " + topAmmendmentText;
                }
                if (councel == "ENV")
                {
                    string topAmmendmentText = am![0].amendments.FirstOrDefault(x => x.ResolutionID == _speakerControl!.CurrentResolutionENV!).Change;
                    string clasue = am![0].amendments.FirstOrDefault(x => x.ResolutionID == _speakerControl!.CurrentResolutionENV!).ClauseNumber;
                    return "(" + clasue + ") " + topAmmendmentText;
                }
                if (councel == "HE")
                {
                    string topAmmendmentText = am![0].amendments.FirstOrDefault(x => x.ResolutionID == _speakerControl!.CurrentResolutionHE!).Change;
                    string clasue = am![0].amendments.FirstOrDefault(x => x.ResolutionID == _speakerControl!.CurrentResolutionHE!).ClauseNumber;
                    return "(" + clasue + ") " + topAmmendmentText;
                }
                if (councel == "SEC")
                {
                    string topAmmendmentText = am![0].amendments.FirstOrDefault(x => x.ResolutionID == _speakerControl!.CurrentResolutionSEC!).Change;
                    string clasue = am![0].amendments.FirstOrDefault(x => x.ResolutionID == _speakerControl!.CurrentResolutionSEC!).ClauseNumber;
                    return "(" + clasue + ") " + topAmmendmentText;
                }
            }
            return "";
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
                Delegation? topContry;
                if (_speakerControl.topSpeakerListGEN.Count() > 0)
                {
                    topContry = delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.topSpeakerListGEN[0]);
                    _speakerControl.topSpeakerListGEN.RemoveAt(0);
                }
                else
                {
                    topContry = delegationsDBContext.Delegation.Where(x => x.RequestedToSpeakGEN == true)
                                       .OrderBy(x => x.TimesSpoken)
                                       .ThenBy(x => x.Login)
                                       .FirstOrDefault();
                }

                if (topContry != null)
                {
                    topContry.TimesSpoken += 1;
                    topContry.RequestedToSpeakGEN = false;

                    await delegationsDBContext.SaveChangesAsync();
                }
            }

            if (councel == "ECO")
            {
                Delegation? topContry;
                if (_speakerControl.topSpeakerListECO.Count() > 0)
                {
                    topContry = delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.topSpeakerListECO[0]);
                    _speakerControl.topSpeakerListECO.RemoveAt(0);
                }
                else
                {
                    topContry = delegationsDBContext.Delegation.Where(x => x.RequestedToSpeakECO == true)
                                       .OrderBy(x => x.TimesSpoken)
                                       .ThenBy(x => x.Login)
                                       .FirstOrDefault();
                }

                if (topContry != null)
                {
                    topContry.TimesSpoken += 1;
                    topContry.RequestedToSpeakECO = false;

                    await delegationsDBContext.SaveChangesAsync();
                }
            }

            if (councel == "ENV")
            {
                Delegation? topContry;
                if (_speakerControl.topSpeakerListENV.Count() > 0)
                {
                    topContry = delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.topSpeakerListENV[0]);
                    _speakerControl.topSpeakerListENV.RemoveAt(0);
                }
                else
                {
                    topContry = delegationsDBContext.Delegation.Where(x => x.RequestedToSpeakENV == true)
                                       .OrderBy(x => x.TimesSpoken)
                                       .ThenBy(x => x.Login)
                                       .FirstOrDefault();
                }

                if (topContry != null)
                {
                topContry.TimesSpoken += 1;
                topContry.RequestedToSpeakENV = false;

                await delegationsDBContext.SaveChangesAsync();
                }
            }

            if (councel == "HE")
            {
                Delegation? topContry;
                if (_speakerControl.topSpeakerListHE.Count() > 0)
                {
                    topContry = delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.topSpeakerListHE[0]);
                    _speakerControl.topSpeakerListHE.RemoveAt(0);
                }
                else
                {
                    topContry = delegationsDBContext.Delegation.Where(x => x.RequestedToSpeakHE == true)
                                        .OrderBy(x => x.TimesSpoken)
                                        .ThenBy(x => x.Login)
                                        .FirstOrDefault();
                }

                if (topContry != null)
                {
                    topContry.TimesSpoken += 1;
                    topContry.RequestedToSpeakHE = false;

                    await delegationsDBContext.SaveChangesAsync();
                }
            }

            if (councel == "SEC")
            {
                Delegation? topContry;
                if (_speakerControl.topSpeakerListSEC.Count() > 0)
                {
                    topContry = delegationsDBContext.Delegation.First(x => x.Country == _speakerControl.topSpeakerListSEC[0]);
                    _speakerControl.topSpeakerListSEC.RemoveAt(0);
                }
                else
                {
                    topContry = delegationsDBContext.Delegation.Where(x => x.RequestedToSpeakSEC == true)
                                       .OrderBy(x => x.TimesSpoken)
                                       .ThenBy(x => x.Login)
                                       .FirstOrDefault();
                }

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

                _speakerControl.topSpeakerListGEN = new List<string>();
            }

            if (councel == "ECO")
            {
                var delegations = await delegationsDBContext.Delegation.Where(d => d.RequestedToSpeakECO == true)
                                                           .ToListAsync();

                foreach (var delegation in delegations)
                {
                    delegation.RequestedToSpeakECO = false;
                }

                _speakerControl.topSpeakerListECO = new List<string>();

            }

            if (councel == "ENV")
            {
                var delegations = await delegationsDBContext.Delegation.Where(d => d.RequestedToSpeakENV == true)
                                                           .ToListAsync();

                foreach (var delegation in delegations)
                {
                    delegation.RequestedToSpeakENV = false;
                }

                _speakerControl.topSpeakerListENV = new List<string>();

            }

            if (councel == "HE")
            {
                var delegations = await delegationsDBContext.Delegation.Where(d => d.RequestedToSpeakHE == true)
                                                           .ToListAsync();

                foreach (var delegation in delegations)
                {
                    delegation.RequestedToSpeakHE = false;
                }

                _speakerControl.topSpeakerListHE = new List<string>();

            }

            if (councel == "SEC")
            {
                var delegations = await delegationsDBContext.Delegation.Where(d => d.RequestedToSpeakSEC == true)
                                                           .ToListAsync();

                foreach (var delegation in delegations)
                {
                    delegation.RequestedToSpeakSEC = false;
                }

                _speakerControl.topSpeakerListSEC = new List<string>();

            }


            await delegationsDBContext.SaveChangesAsync();

            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Lock(string councel)
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            if (councel == "GEN")
            {
                _speakerControl.allowSpeakRequestsGEN = false;
            }
            if (councel == "ECO")
            {
                _speakerControl.allowSpeakRequestsECO = false;
            }
            if (councel == "ENV")
            {
                _speakerControl.allowSpeakRequestsENV = false;
            }
            if (councel == "HE")
            {
                _speakerControl.allowSpeakRequestsHE = false;
            }
            if (councel == "SEC")
            {
                _speakerControl.allowSpeakRequestsSEC = false;
            }

            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Unlock(string councel)
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            if (councel == "GEN")
            {
                _speakerControl.allowSpeakRequestsGEN = true;
            }
            if (councel == "ECO")
            {
                _speakerControl.allowSpeakRequestsECO = true;
            }
            if (councel == "ENV")
            {
                _speakerControl.allowSpeakRequestsENV = true;
            }
            if (councel == "HE")
            {
                _speakerControl.allowSpeakRequestsHE = true;
            }
            if (councel == "SEC")
            {
                _speakerControl.allowSpeakRequestsSEC = true;
            }

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

        public IActionResult SetCurrentResolution(int resID, string councel)
        {
            if (councel == "GEN")
            {
                _speakerControl.CurrentResolutionGEN = resID;
            }
            if (councel == "ECO")
            {
                _speakerControl.CurrentResolutionECO = resID;
            }
            if (councel == "ENV")
            {
                _speakerControl.CurrentResolutionENV = resID;
            }
            if (councel == "HE")
            {
                _speakerControl.CurrentResolutionHE = resID;
            }
            if (councel == "SEC")
            {
                _speakerControl.CurrentResolutionSEC = resID;
            }
            return RedirectToAction("SpeakerOrderDashboard", "SpeakerOrder", new { curRes = resID });
        }
    }
}
