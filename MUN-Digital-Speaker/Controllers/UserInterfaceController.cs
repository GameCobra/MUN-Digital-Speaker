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
        private readonly MUN_Digital_SpeakerContext delegationsDBContext;
        private readonly SpeakerListControlStates _speakerControl;

        DelegationLookup delLookup;


        public UserInterfaceController(MUN_Digital_SpeakerContext context, SpeakerListControlStates speakerControl)
        {
            delegationsDBContext = context;
            _speakerControl = speakerControl;
            delLookup = new DelegationLookup(delegationsDBContext);

        }

        public IActionResult Index()
        {
            return RedirectToAction(actionName: nameof(Dashboard)); //routeValues: new {loginID = 0, key = 123}
        }

        [HttpPost]
        public async Task<IActionResult> SpeakRequest(SpeakRequest requested)
        {
            if (_speakerControl.allowSpeakRequests == false)
            {
                return RedirectToAction(actionName: nameof(Dashboard), new { message = "locked" });
            }
            Delegation delegation = await delegationsDBContext.Delegation.FirstAsync(x => x.Login == requested.Login);
            delegation.RequestedToSpeak = true;
            if (requested.IsRevoking)
            {
                delegation.RequestedToSpeak = false;
            }
            DelegationsController delegationController = new DelegationsController(delegationsDBContext);

            try
            {
                delegationsDBContext.Update(delegation);
                await delegationsDBContext.SaveChangesAsync();
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

            return RedirectToAction(actionName: nameof(Dashboard), new {message = "Successfully requested to speak" });
        }

        [Authorize]
        public async Task<IActionResult> Dashboard(string message)
        {
            #pragma warning disable 8603, 8602
            Delegation? loggedInDelegation = await delegationsDBContext.Delegation.FirstAsync(x => x.Login.ToString() == User.Identity.Name);
            #pragma warning restore 8603, 8602

            ViewData["country"] = loggedInDelegation.Country;
            ViewData["login"] = loggedInDelegation.Login;
            ViewData["hasRequested"] = loggedInDelegation.RequestedToSpeak;
            ViewData["message"] = message;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SubmitAmmendment(string? change, int resoltuionID, string? ClauseNumber)
        {
            if (change == null || ClauseNumber == null)
            {
                return RedirectToAction(actionName: nameof(ViewAmendments));
            }

            Delegation currentDelegation = await delegationsDBContext.Delegation.FirstAsync(x => x.Login.ToString() == User.Identity.Name);
            if (currentDelegation.amendments == null)
            {
                currentDelegation.amendments = new List<Amendment>();
            }
            currentDelegation.amendments.Add(new Amendment { Change = change, ResolutionID = resoltuionID, ClauseNumber = ClauseNumber});
            delegationsDBContext.Update(currentDelegation);
            await delegationsDBContext.SaveChangesAsync();
            return RedirectToAction(actionName: nameof(ViewAmendments));
        }
        [Authorize]
        public async Task<IActionResult> ViewAmendments()
        {
            List<Resolution> resolutions = await delLookup.LoadResolutionsAsync();

            Delegation delegation = await delegationsDBContext.Delegation.FirstOrDefaultAsync(x => x.Login.ToString() == User.Identity.Name);

            ResolutionDelegationViewModel viewModel = new ResolutionDelegationViewModel()
            {
                resolutions = resolutions,
                delegation = delegation
            };

            //System.Diagnostics.Debug.WriteLine("VIEW MODEL >> " + viewModel.resolutions[0].Name);

            return View(viewModel);


            //NOTE: may want to add the ability to see other delegations ammendments
            //This code was that, probably
            /*
            //if the id is not given, set the id to the current user
            if (id == null)
            {
                id = int.Parse(User.Identity.Name);
            }
            
            
            
            Delegation del = await delegationsDBContext.Delegation.FirstOrDefaultAsync(x => x.Login == id);
            
            if (del == null)
            {
                del = new Delegation { Id = (int)id };
            }
            
            if (del.amendments == null)
            {
                del.amendments = new List<Amendment>();
            }
            */
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteResolution(int delegationID, string change)
        {

            Delegation delegationToEdit = await delegationsDBContext.Delegation.FirstAsync(x => x.Id == delegationID);

            //PLEASE do not have two amendments with the same change
            //Then deleting them will deleat both
            //Probably a small enough bug to ignore for now :)

            delegationToEdit.amendments.RemoveAll(x => x.Change == change);

            delegationsDBContext.Update(delegationToEdit);
            await delegationsDBContext.SaveChangesAsync();

            return RedirectToAction(actionName: nameof(ViewAmendments));
        }
    }
}