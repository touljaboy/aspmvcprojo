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
    public class KlasyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KlasyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Klasy
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Klasy.Include(k => k.Wychowawca);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Klasy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klasa = await _context.Klasy
                .Include(k => k.Wychowawca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klasa == null)
            {
                return NotFound();
            }

            return View(klasa);
        }

        // GET: Klasy/Create
        public IActionResult Create()
        {
            var nauczyciele = _context.Nauczyciele
                .Select(n => new { n.Id, Display = n.Imie + " " + n.Nazwisko })
                .ToList();
            ViewData["WychowawcaId"] = new SelectList(nauczyciele, "Id", "Display");
            return View();
        }

        // POST: Klasy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NazwaKlasy,WychowawcaId")] Klasa klasa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klasa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var nauczyciele = _context.Nauczyciele
                .Select(n => new { n.Id, Display = n.Imie + " " + n.Nazwisko })
                .ToList();
            ViewData["WychowawcaId"] = new SelectList(nauczyciele, "Id", "Display", klasa.WychowawcaId);
            return View(klasa);
        }

        // GET: Klasy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klasa = await _context.Klasy.FindAsync(id);
            if (klasa == null)
            {
                return NotFound();
            }
            var nauczyciele = _context.Nauczyciele
                .Select(n => new { n.Id, Display = n.Imie + " " + n.Nazwisko })
                .ToList();
            ViewData["WychowawcaId"] = new SelectList(nauczyciele, "Id", "Display", klasa.WychowawcaId);
            return View(klasa);
        }

        // POST: Klasy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NazwaKlasy,WychowawcaId")] Klasa klasa)
        {
            if (id != klasa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klasa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlasaExists(klasa.Id))
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
            ViewData["WychowawcaId"] = new SelectList(_context.Nauczyciele, "Id", "Id", klasa.WychowawcaId);
            return View(klasa);
        }

        // GET: Klasy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klasa = await _context.Klasy
                .Include(k => k.Wychowawca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klasa == null)
            {
                return NotFound();
            }

            return View(klasa);
        }

        // POST: Klasy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klasa = await _context.Klasy.FindAsync(id);
            if (klasa != null)
            {
                _context.Klasy.Remove(klasa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlasaExists(int id)
        {
            return _context.Klasy.Any(e => e.Id == id);
        }
    }
}
