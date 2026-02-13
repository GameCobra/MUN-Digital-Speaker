using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MUN_Digital_Speaker.Data;
using MUN_Digital_Speaker.Models;

namespace MUN_Digital_Speaker.Controllers
{
    public class DelegationsController : Controller
    {
        // Context for the central database
        private readonly MUN_Digital_SpeakerContext delegationsDBContext;

        // Helper function to look up delegations
        DelegationLookup delLookup;
        private readonly SpeakerListControlStates? _speakerControl;


        // Init Function
        public DelegationsController(MUN_Digital_SpeakerContext context, SpeakerListControlStates? speakerControl)
        {
            delegationsDBContext = context;
            delLookup = new DelegationLookup(delegationsDBContext);
            _speakerControl = speakerControl;

        }

        // Page allowing the admin account to view all delegations
        [Authorize]
        public async Task<IActionResult> Index()
        {
            bool isAdmin = await delLookup.IsAdmin(User.Identity.Name);
            if (!isAdmin)
            {
                return Unauthorized();
            }

            List<Delegation> delegationsList = await delegationsDBContext.Delegation.ToListAsync();
            var DupliciteLoginValues = delegationsList.GroupBy(x => x.Login)
                                                .Where(x => x.Count() > 1)
                                                .Select(x => x.Key);
            //System.Diagnostics.Debug.WriteLine("DUPLICIT >> " + firstDuplicite.ToList());
            
            if (DupliciteLoginValues.Count() > 0)
            {
                ViewData["DuplicitLogins"] = DupliciteLoginValues.ToList();
            }
            else
            {
                ViewData["DuplicitLogins"] = null;
            }

            var DupliciteContryValues = delegationsList.GroupBy(x => x.Country)
                                    .Where(x => x.Count() > 1)
                                    .Select(x => x.Key);
            //System.Diagnostics.Debug.WriteLine("DUPLICIT >> " + firstDuplicite.ToList());

            if (DupliciteContryValues.Count() > 0)
            {
                ViewData["DuplicitContries"] = DupliciteContryValues.ToList();
            }
            else
            {
                ViewData["DuplicitContries"] = null;
            }


            return View(await delegationsDBContext.Delegation.ToListAsync());
        }

        // Allows the admin account to view the specific details of an account
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            var investigatingDelegation = await delegationsDBContext.Delegation.FirstOrDefaultAsync(m => m.Id == id);
            if (investigatingDelegation == null)
            {
                return NotFound();
            }

            return View(investigatingDelegation);
        }

        // The viewable portion of the Create page
        [Authorize]
        public async Task<IActionResult> Create()
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            return View();
        }

        // Allows the admin account to create delegations
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> Create([Bind("Id,Login,Key,Country,TimesSpoken,AmendmentPoints")] Delegation delegation)
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            bool isDupliciteLogin = await delegationsDBContext.Delegation.AnyAsync(x => x.Login == delegation.Login);

            if (isDupliciteLogin)
            {
                ModelState.AddModelError("Login", "That login already exists.");
            }

            bool isDupliciteCountry = await delegationsDBContext.Delegation.AnyAsync(x => x.Country == delegation.Country);

            if (isDupliciteCountry)
            {
                ModelState.AddModelError("Country", "That country already exists.");
            }

            delegation.AmendmentPoints = 0;


            if (ModelState.IsValid)
            {
                delegationsDBContext.Add(delegation);
                await delegationsDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delegation);
        }

        // Allows the admin account to view the edit page
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            var investigatingDelegation = await delegationsDBContext.Delegation.FindAsync(id);
            if (investigatingDelegation == null)
            {
                return NotFound();
            }
            return View(investigatingDelegation);
        }

        // Allows the admin account to edit delegations
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,Key,Country,TimesSpoken,AmendmentPoints")] Delegation delegation)
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            if (id != delegation.Id)
            {
                return NotFound();
            }

            bool isDupliciteLogin = await delegationsDBContext.Delegation.AsNoTracking().AnyAsync(x => x.Id != id && x.Login == delegation.Login);
            
            if (isDupliciteLogin)
            {
                ModelState.AddModelError("Login", "That login already exists.");
            }

            bool isDupliciteCountry = await delegationsDBContext.Delegation.AsNoTracking().AnyAsync(x => x.Id != id && x.Country == delegation.Country);

            if (isDupliciteCountry)
            {
                ModelState.AddModelError("Country", "That country already exists.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    delegationsDBContext.Update(delegation);
                    await delegationsDBContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DelegationExists(delegation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(delegation);
        }

        // Allows the admin account to view the delete page
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var investigatingDelegation = await delegationsDBContext.Delegation.FirstOrDefaultAsync(m => m.Id == id);
            if (investigatingDelegation == null)
            {
                return NotFound();
            }

            return View(investigatingDelegation);
        }

        // Allows the admin account to delete delegations
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            var investigatingDelegation = await delegationsDBContext.Delegation.FindAsync(id);
            if (investigatingDelegation != null)
            {
                delegationsDBContext.Delegation.Remove(investigatingDelegation);
                await delegationsDBContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // returns true if a delegation already exists
        public bool DelegationExists(int id)
        {
            return delegationsDBContext.Delegation.Any(e => e.Id == id);
        }

        // Allows the admin account to view the uploading page
        public async Task<IActionResult> Upload()
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            return View();
        }

        // Allows the admin account to upload .csv to auto populate delegations into the system
        [HttpPost, Authorize]
        public async Task<IActionResult> Upload(UploadModel model)
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            if (model.File == null)
            {
                ViewData["Message"] = "No file selected.";
                return View();
            }

            if (model.File.Length == 0)
            {
                ViewData["Message"] = "File has no data.";
                return View();
            }

            var records = new List<string[]>();

            using (var stream = new StreamReader(model.File.OpenReadStream()))
            {
                while (!stream.EndOfStream)
                {
                    var line = await stream.ReadLineAsync();
                    var values = line.Split(','); // split CSV by commas
                    records.Add(values);
                }
            }
            for (int i = 0; i < records.Count; i++)
            {
                Delegation parseDelegation = new Delegation { Country = records[i][0], Login = int.Parse(records[i][1]), Key = int.Parse(records[i][2]), TimesSpoken = 0, AmendmentPoints = 0};
                delegationsDBContext.Add(parseDelegation);
            }

            await delegationsDBContext.SaveChangesAsync();
            ViewData["Message"] = "Upload Succsesful";

            return RedirectToAction("Index", "Delegations");


            //var filePath = Path.Combine("wwwroot/uploads", model.File.FileName);
            /*
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }*/

            ViewData["Message"] = "File uploaded successfully!";
        }

        [Authorize]
        public async Task<IActionResult> SetResolutionsToDefault()
        {
            if (!await delLookup.IsAdmin(User.Identity.Name))
            {
                return Unauthorized();
            }

            _speakerControl!.Resolutions = await delLookup.LoadResolutionsAsync();
            return RedirectToAction("Index", "Delegations");
        }
    }
}