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
    public class CountryListsController : Controller
    {
        private readonly DemoDbContext _context;

        public CountryListsController(DemoDbContext context)
        {
            _context = context;
        }

        // GET: CountryLists
        public async Task<IActionResult> Index()
        {
              return _context.CountryLists != null ? 
                          View(await _context.CountryLists.ToListAsync()) :
                          Problem("Entity set 'DemoDbContext.CountryLists'  is null.");
        }

        // GET: CountryLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CountryLists == null)
            {
                return NotFound();
            }

            var countryList = await _context.CountryLists
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (countryList == null)
            {
                return NotFound();
            }

            return View(countryList);
        }

        // GET: CountryLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CountryLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryId,CountryName")] CountryList countryList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(countryList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(countryList);
        }

        // GET: CountryLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CountryLists == null)
            {
                return NotFound();
            }

            var countryList = await _context.CountryLists.FindAsync(id);
            if (countryList == null)
            {
                return NotFound();
            }
            return View(countryList);
        }

        // POST: CountryLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryId,CountryName")] CountryList countryList)
        {
            if (id != countryList.CountryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countryList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryListExists(countryList.CountryId))
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
            return View(countryList);
        }

        // GET: CountryLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CountryLists == null)
            {
                return NotFound();
            }

            var countryList = await _context.CountryLists
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (countryList == null)
            {
                return NotFound();
            }

            return View(countryList);
        }

        // POST: CountryLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CountryLists == null)
            {
                return Problem("Entity set 'DemoDbContext.CountryLists'  is null.");
            }
            var countryList = await _context.CountryLists.FindAsync(id);
            if (countryList != null)
            {
                _context.CountryLists.Remove(countryList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryListExists(int id)
        {
          return (_context.CountryLists?.Any(e => e.CountryId == id)).GetValueOrDefault();
        }
    }
}
