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
    public class OcenyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OcenyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Oceny
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Oceny
                .Include(o => o.Przedmiot)
                .Include(o => o.Uczen);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Oceny/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ocena = await _context.Oceny
                .Include(o => o.Przedmiot)
                .Include(o => o.Uczen)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (ocena == null) return NotFound();

            return View(ocena);
        }

        // GET: Oceny/Create
        public IActionResult Create()
        {
            // POPRAWKA: Wyświetlamy "Nazwa" przedmiotu zamiast "Id"
            ViewData["PrzedmiotId"] = new SelectList(_context.Przedmioty, "Id", "Nazwa");
            // POPRAWKA: Wyświetlamy "Nazwisko" ucznia zamiast "Id"
            ViewData["UczenId"] = new SelectList(_context.Uczniowie, "Id", "Nazwisko");
            return View();
        }

        // POST: Oceny/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Wartosc,Opis,Data,UczenId,PrzedmiotId")] Ocena ocena)
        {
            // --- FIX WALIDACJI ---
            // Ignorujemy brak pełnych obiektów, bo mamy tylko ID
            ModelState.Remove("Uczen");
            ModelState.Remove("Przedmiot");

            if (ModelState.IsValid)
            {
                _context.Add(ocena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            // Odnawianie listy w razie błędu (z czytelnymi nazwami)
            ViewData["PrzedmiotId"] = new SelectList(_context.Przedmioty, "Id", "Nazwa", ocena.PrzedmiotId);
            ViewData["UczenId"] = new SelectList(_context.Uczniowie, "Id", "Nazwisko", ocena.UczenId);
            return View(ocena);
        }

        // GET: Oceny/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ocena = await _context.Oceny.FindAsync(id);
            if (ocena == null) return NotFound();
            
            // POPRAWKA: Listy przy edycji
            ViewData["PrzedmiotId"] = new SelectList(_context.Przedmioty, "Id", "Nazwa", ocena.PrzedmiotId);
            ViewData["UczenId"] = new SelectList(_context.Uczniowie, "Id", "Nazwisko", ocena.UczenId);
            return View(ocena);
        }

        // POST: Oceny/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Wartosc,Opis,Data,UczenId,PrzedmiotId")] Ocena ocena)
        {
            if (id != ocena.Id) return NotFound();

            // --- FIX WALIDACJI ---
            ModelState.Remove("Uczen");
            ModelState.Remove("Przedmiot");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcenaExists(ocena.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrzedmiotId"] = new SelectList(_context.Przedmioty, "Id", "Nazwa", ocena.PrzedmiotId);
            ViewData["UczenId"] = new SelectList(_context.Uczniowie, "Id", "Nazwisko", ocena.UczenId);
            return View(ocena);
        }

        // GET: Oceny/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ocena = await _context.Oceny
                .Include(o => o.Przedmiot)
                .Include(o => o.Uczen)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (ocena == null) return NotFound();

            return View(ocena);
        }

        // POST: Oceny/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ocena = await _context.Oceny.FindAsync(id);
            if (ocena != null)
            {
                _context.Oceny.Remove(ocena);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcenaExists(int id)
        {
            return _context.Oceny.Any(e => e.Id == id);
        }
    }
}