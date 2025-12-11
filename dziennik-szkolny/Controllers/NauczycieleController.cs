using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dziennik_szkolny.Data;
using Dziennik_szkolny.Models;

namespace dziennik_szkolny.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NauczycieleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NauczycieleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nauczyciele
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Nauczyciele.Include(n => n.Konto).Include(n => n.Przelozony);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Nauczyciele/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nauczyciel = await _context.Nauczyciele
                .Include(n => n.Konto)
                .Include(n => n.Przelozony)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nauczyciel == null)
            {
                return NotFound();
            }

            return View(nauczyciel);
        }

        // GET: Nauczyciele/Create
        public IActionResult Create()
        {
            ViewData["KontoId"] = new SelectList(_context.Konto, "Id", "Nazwa");
            var nauczyciele = _context.Nauczyciele
                .Select(n => new { n.Id, Display = n.Imie + " " + n.Nazwisko })
                .ToList();
            ViewData["PrzelozonyId"] = new SelectList(nauczyciele, "Id", "Display");
            return View();
        }

        // POST: Nauczyciele/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,Email,Telefon,CzyWychowawca,KontoId,PrzelozonyId")] Nauczyciel nauczyciel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nauczyciel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KontoId"] = new SelectList(_context.Konto, "Id", "Nazwa", nauczyciel.KontoId);
            var nauczyciele = _context.Nauczyciele
                .Select(n => new { n.Id, Display = n.Imie + " " + n.Nazwisko })
                .ToList();
            ViewData["PrzelozonyId"] = new SelectList(nauczyciele, "Id", "Display", nauczyciel.PrzelozonyId);
            return View(nauczyciel);
        }

        // GET: Nauczyciele/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nauczyciel = await _context.Nauczyciele.FindAsync(id);
            if (nauczyciel == null)
            {
                return NotFound();
            }
            ViewData["KontoId"] = new SelectList(_context.Konto, "Id", "Nazwa", nauczyciel.KontoId);
            var nauczyciele = _context.Nauczyciele
                .Where(n => n.Id != id)
                .Select(n => new { n.Id, Display = n.Imie + " " + n.Nazwisko })
                .ToList();
            ViewData["PrzelozonyId"] = new SelectList(nauczyciele, "Id", "Display", nauczyciel.PrzelozonyId);
            return View(nauczyciel);
        }

        // POST: Nauczyciele/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,Email,Telefon,CzyWychowawca,KontoId,PrzelozonyId")] Nauczyciel nauczyciel)
        {
            if (id != nauczyciel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nauczyciel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NauczycielExists(nauczyciel.Id))
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
            ViewData["KontoId"] = new SelectList(_context.Konto, "Id", "Nazwa", nauczyciel.KontoId);
            var nauczyciele = _context.Nauczyciele
                .Select(n => new { n.Id, Display = n.Imie + " " + n.Nazwisko })
                .ToList();
            ViewData["PrzelozonyId"] = new SelectList(nauczyciele, "Id", "Display", nauczyciel.PrzelozonyId);
            return View(nauczyciel);
        }

        // GET: Nauczyciele/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nauczyciel = await _context.Nauczyciele
                .Include(n => n.Konto)
                .Include(n => n.Przelozony)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nauczyciel == null)
            {
                return NotFound();
            }

            return View(nauczyciel);
        }

        // POST: Nauczyciele/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nauczyciel = await _context.Nauczyciele.FindAsync(id);
            if (nauczyciel != null)
            {
                _context.Nauczyciele.Remove(nauczyciel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NauczycielExists(int id)
        {
            return _context.Nauczyciele.Any(e => e.Id == id);
        }
    }
}
