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
            var applicationDbContext = _context.Oceny.Include(o => o.Nauczyciel).Include(o => o.Przedmiot).Include(o => o.Uczen);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Oceny/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocena = await _context.Oceny
                .Include(o => o.Nauczyciel)
                .Include(o => o.Przedmiot)
                .Include(o => o.Uczen)
                .FirstOrDefaultAsync(m => m.Id_oceny == id);
            if (ocena == null)
            {
                return NotFound();
            }

            return View(ocena);
        }

        // GET: Oceny/Create
        public IActionResult Create()
        {
            ViewData["Id_nauczyciela"] = new SelectList(_context.Nauczyciele, "Id_wychowawcy", "Imie");
            ViewData["Id_przedmiotu"] = new SelectList(_context.Przedmioty, "Id_przedmiotu", "NazwaPrzedmiotu");
            ViewData["Id_Ucznia"] = new SelectList(_context.Uczniowie, "Id_Ucznia", "Id_Ucznia");
            return View();
        }

        // POST: Oceny/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_oceny,Wartosc,DataWpisu,Id_Ucznia,Id_przedmiotu,Id_nauczyciela")] Ocena ocena)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ocena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_nauczyciela"] = new SelectList(_context.Nauczyciele, "Id_wychowawcy", "Imie", ocena.Id_nauczyciela);
            ViewData["Id_przedmiotu"] = new SelectList(_context.Przedmioty, "Id_przedmiotu", "NazwaPrzedmiotu", ocena.Id_przedmiotu);
            ViewData["Id_Ucznia"] = new SelectList(_context.Uczniowie, "Id_Ucznia", "Id_Ucznia", ocena.Id_Ucznia);
            return View(ocena);
        }

        // GET: Oceny/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocena = await _context.Oceny.FindAsync(id);
            if (ocena == null)
            {
                return NotFound();
            }
            ViewData["Id_nauczyciela"] = new SelectList(_context.Nauczyciele, "Id_wychowawcy", "Imie", ocena.Id_nauczyciela);
            ViewData["Id_przedmiotu"] = new SelectList(_context.Przedmioty, "Id_przedmiotu", "NazwaPrzedmiotu", ocena.Id_przedmiotu);
            ViewData["Id_Ucznia"] = new SelectList(_context.Uczniowie, "Id_Ucznia", "Id_Ucznia", ocena.Id_Ucznia);
            return View(ocena);
        }

        // POST: Oceny/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_oceny,Wartosc,DataWpisu,Id_Ucznia,Id_przedmiotu,Id_nauczyciela")] Ocena ocena)
        {
            if (id != ocena.Id_oceny)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcenaExists(ocena.Id_oceny))
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
            ViewData["Id_nauczyciela"] = new SelectList(_context.Nauczyciele, "Id_wychowawcy", "Imie", ocena.Id_nauczyciela);
            ViewData["Id_przedmiotu"] = new SelectList(_context.Przedmioty, "Id_przedmiotu", "NazwaPrzedmiotu", ocena.Id_przedmiotu);
            ViewData["Id_Ucznia"] = new SelectList(_context.Uczniowie, "Id_Ucznia", "Id_Ucznia", ocena.Id_Ucznia);
            return View(ocena);
        }

        // GET: Oceny/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocena = await _context.Oceny
                .Include(o => o.Nauczyciel)
                .Include(o => o.Przedmiot)
                .Include(o => o.Uczen)
                .FirstOrDefaultAsync(m => m.Id_oceny == id);
            if (ocena == null)
            {
                return NotFound();
            }

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
            return _context.Oceny.Any(e => e.Id_oceny == id);
        }
    }
}
