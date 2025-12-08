using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dziennik_szkolny.Data;
using Dziennik_szkolny.Models;

namespace Dziennik_szkolny.Controllers
{
    public class KontaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KontaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Konta
        public async Task<IActionResult> Index()
        {
            // Konto nie ma kluczy obcych, więc nie musimy robić .Include()
            return View(await _context.Konto.ToListAsync());
        }

        // GET: Konta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var konto = await _context.Konto
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (konto == null) return NotFound();

            return View(konto);
        }

        // GET: Konta/Create
        public IActionResult Create()
        {
            // Tutaj NIE POTRZEBUJESZ żadnych SelectList, 
            // bo Konto nie zależy od nikogo innego.
            return View();
        }

        // POST: Konta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,HasloHash,Rola")] Konto konto)
        {
            // Zabezpieczenie: jeśli w przyszłości dodasz relacje zwrotne w modelu
            // (np. public ICollection<Uczen>...), to walidator mógłby krzyczeć.
            // Te linie zapobiegają błędom:
            ModelState.Remove("Uczniowie");
            ModelState.Remove("Nauczyciele");
            ModelState.Remove("Rodzice");

            if (ModelState.IsValid)
            {
                _context.Add(konto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(konto);
        }

        // GET: Konta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var konto = await _context.Konto.FindAsync(id);
            if (konto == null) return NotFound();
            
            return View(konto);
        }

        // POST: Konta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,HasloHash,Rola")] Konto konto)
        {
            if (id != konto.Id) return NotFound();

            // Zabezpieczenie walidacji
            ModelState.Remove("Uczniowie");
            ModelState.Remove("Nauczyciele");
            ModelState.Remove("Rodzice");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(konto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KontoExists(konto.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(konto);
        }

        // GET: Konta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var konto = await _context.Konto
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (konto == null) return NotFound();

            return View(konto);
        }

        // POST: Konta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var konto = await _context.Konto.FindAsync(id);
            if (konto != null)
            {
                _context.Konto.Remove(konto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KontoExists(int id)
        {
            return _context.Konto.Any(e => e.Id == id);
        }
    }
}