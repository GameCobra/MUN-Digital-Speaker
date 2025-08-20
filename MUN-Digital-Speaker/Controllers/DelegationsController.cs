using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MUN_Digital_Speaker.Data;
using MUN_Digital_Speaker.Models;

namespace MUN_Digital_Speaker.Controllers
{
    public class DelegationsController : Controller
    {
        private readonly MUN_Digital_SpeakerContext _context;

        public DelegationsController(MUN_Digital_SpeakerContext context)
        {
            _context = context;
        }

        // GET: Delegations
        public async Task<IActionResult> Index()
        {
            List<Delegation> delegations = await _context.Delegation.ToListAsync();
            var firstDuplicite = delegations.GroupBy(x => x.Login)
                                            .FirstOrDefault(x => x.Count() >= 2);
            //System.Diagnostics.Debug.WriteLine(firstDuplicite);
            
            if (firstDuplicite != null)
            {
                ViewData["hasDuplicitLogins"] = firstDuplicite.Key;
            }
            else
            {
                ViewData["hasDuplicitLogins"] = null;
            }

            return View(await _context.Delegation.ToListAsync());
        }

        // GET: Delegations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delegation = await _context.Delegation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delegation == null)
            {
                return NotFound();
            }

            return View(delegation);
        }

        // GET: Delegations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Delegations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Key,Country,TimesSpoken,RequestedToSpeak")] Delegation delegation)
        {
            bool isError = false;

            bool isDupliciteLogin = await _context.Delegation.AnyAsync(x => x.Login == delegation.Login);

            if (isDupliciteLogin)
            {
                ModelState.AddModelError("Login", "That login already exists.");
                isError = true;
            }

            bool isDupliciteCountry = await _context.Delegation.AnyAsync(x => x.Country == delegation.Country);

            if (isDupliciteCountry)
            {
                ModelState.AddModelError("Country", "That country already exists.");
                isError = true;
            }


            if (ModelState.IsValid && !isError)
            {
                _context.Add(delegation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delegation);
        }

        // GET: Delegations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delegation = await _context.Delegation.FindAsync(id);
            if (delegation == null)
            {
                return NotFound();
            }
            return View(delegation);
        }

        // POST: Delegations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,Key,Country,TimesSpoken,RequestedToSpeak")] Delegation delegation)
        {
            bool isError = false;

            if (id != delegation.Id)
            {
                return NotFound();
            }

            bool isDupliciteLogin = await _context.Delegation.AnyAsync(x => x.Id != id && x.Login == delegation.Login);
            
            if (isDupliciteLogin)
            {
                ModelState.AddModelError("Login", "That login already exists.");
                isError = true;
            }

            bool isDupliciteCountry = await _context.Delegation.AnyAsync(x => x.Id != id && x.Country == delegation.Country);

            if (isDupliciteCountry)
            {
                ModelState.AddModelError("Country", "That country already exists.");
                isError = true;
            }

            if (ModelState.IsValid && !isError)
            {
                try
                {
                    _context.Update(delegation);
                    await _context.SaveChangesAsync();
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

        // GET: Delegations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delegation = await _context.Delegation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delegation == null)
            {
                return NotFound();
            }

            return View(delegation);
        }

        // POST: Delegations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delegation = await _context.Delegation.FindAsync(id);
            if (delegation != null)
            {
                _context.Delegation.Remove(delegation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool DelegationExists(int id)
        {
            return _context.Delegation.Any(e => e.Id == id);
        }
    }
}