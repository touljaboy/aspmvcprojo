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
    public class UczniowieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UczniowieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Uczniowie
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Uczniowie
                .Include(u => u.Klasa)
                .Include(u => u.Konto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Uczniowie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var uczen = await _context.Uczniowie
                .Include(u => u.Klasa)
                .Include(u => u.Konto)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (uczen == null) return NotFound();

            return View(uczen);
        }

        // GET: Uczniowie/Create
        public IActionResult Create()
        {
            // POPRAWKA: Wyświetlamy "NazwaKlasy" zamiast "Id"
            ViewData["KlasaId"] = new SelectList(_context.Klasy, "Id", "NazwaKlasy");
            // POPRAWKA: Wyświetlamy "Nazwa" (login) konta zamiast "Id"
            // Upewnij się, że w DbContext masz tabelę 'Konto' lub 'Konta' - dostosuj nazwę poniżej
            ViewData["KontoId"] = new SelectList(_context.Konto, "Id", "Nazwa"); 
            return View();
        }

        // POST: Uczniowie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,KlasaId,KontoId")] Uczen uczen)
        {
            // --- FIX WALIDACJI ---
            // Ignorujemy brak obiektów, bo przesyłamy tylko ID
            ModelState.Remove("Klasa");
            ModelState.Remove("Konto");
            ModelState.Remove("Oceny"); // Uczeń ma listę ocen, też ją ignorujemy przy tworzeniu

            if (ModelState.IsValid)
            {
                _context.Add(uczen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            // Jak coś pójdzie nie tak, odnawiamy listy z czytelnymi nazwami
            ViewData["KlasaId"] = new SelectList(_context.Klasy, "Id", "NazwaKlasy", uczen.KlasaId);
            ViewData["KontoId"] = new SelectList(_context.Konto, "Id", "Nazwa", uczen.KontoId);
            return View(uczen);
        }

        // GET: Uczniowie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var uczen = await _context.Uczniowie.FindAsync(id);
            if (uczen == null) return NotFound();
            
            // POPRAWKA: Listy przy edycji
            ViewData["KlasaId"] = new SelectList(_context.Klasy, "Id", "NazwaKlasy", uczen.KlasaId);
            ViewData["KontoId"] = new SelectList(_context.Konto, "Id", "Nazwa", uczen.KontoId);
            return View(uczen);
        }

        // POST: Uczniowie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,KlasaId,KontoId")] Uczen uczen)
        {
            if (id != uczen.Id) return NotFound();

            // --- FIX WALIDACJI ---
            ModelState.Remove("Klasa");
            ModelState.Remove("Konto");
            ModelState.Remove("Oceny");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uczen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UczenExists(uczen.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlasaId"] = new SelectList(_context.Klasy, "Id", "NazwaKlasy", uczen.KlasaId);
            ViewData["KontoId"] = new SelectList(_context.Konto, "Id", "Nazwa", uczen.KontoId);
            return View(uczen);
        }

        // GET: Uczniowie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var uczen = await _context.Uczniowie
                .Include(u => u.Klasa)
                .Include(u => u.Konto)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (uczen == null) return NotFound();

            return View(uczen);
        }

        // POST: Uczniowie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uczen = await _context.Uczniowie.FindAsync(id);
            if (uczen != null)
            {
                _context.Uczniowie.Remove(uczen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UczenExists(int id)
        {
            return _context.Uczniowie.Any(e => e.Id == id);
        }
    }
}