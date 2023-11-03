using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VineforceJitendra.Models;

namespace VineforceJitendra.Controllers
{
    public class StateListsController : Controller
    {
        private readonly DemoDbContext _context;

        public StateListsController(DemoDbContext context)
        {
            _context = context;
        }

        // GET: StateLists
        public async Task<IActionResult> Index()
        {
            var demoDbContext = _context.StateLists.Include(s => s.Country);
            return View(await demoDbContext.ToListAsync());
        }

        // GET: StateLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StateLists == null)
            {
                return NotFound();
            }

            var stateList = await _context.StateLists
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (stateList == null)
            {
                return NotFound();
            }

            return View(stateList);
        }

        // GET: StateLists/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.CountryLists, "CountryId", "CountryName");
            return View();
        }

        // POST: StateLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateId,StateName,CountryId")] StateList stateList)
        {
            ModelState.Remove("Country");
            if (ModelState.IsValid)
            {
                _context.Add(stateList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.CountryLists, "CountryId", "CountryName", stateList.CountryId);
            return View(stateList);
        }

        // GET: StateLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StateLists == null)
            {
                return NotFound();
            }

            var stateList = await _context.StateLists.FindAsync(id);
            if (stateList == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.CountryLists, "CountryId", "CountryName", stateList.CountryId);
            return View(stateList);
        }

        // POST: StateLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateId,StateName,CountryId")] StateList stateList)
        {
            ModelState.Remove("Country");
            if (id != stateList.StateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stateList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateListExists(stateList.StateId))
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
            ViewData["CountryId"] = new SelectList(_context.CountryLists, "CountryId", "CountryName", stateList.CountryId);
            return View(stateList);
        }

        // GET: StateLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StateLists == null)
            {
                return NotFound();
            }

            var stateList = await _context.StateLists
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (stateList == null)
            {
                return NotFound();
            }

            return View(stateList);
        }

        // POST: StateLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StateLists == null)
            {
                return Problem("Entity set 'DemoDbContext.StateLists'  is null.");
            }
            var stateList = await _context.StateLists.FindAsync(id);
            if (stateList != null)
            {
                _context.StateLists.Remove(stateList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateListExists(int id)
        {
          return (_context.StateLists?.Any(e => e.StateId == id)).GetValueOrDefault();
        }
    }
}
