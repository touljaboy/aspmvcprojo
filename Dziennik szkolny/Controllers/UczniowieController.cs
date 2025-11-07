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
            var applicationDbContext = _context.Uczniowie.Include(u => u.Klasa).Include(u => u.Rodzic).Include(u => u.Wychowawca);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Uczniowie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uczen = await _context.Uczniowie
                .Include(u => u.Klasa)
                .Include(u => u.Rodzic)
                .Include(u => u.Wychowawca)
                .FirstOrDefaultAsync(m => m.Id_Ucznia == id);
            if (uczen == null)
            {
                return NotFound();
            }

            return View(uczen);
        }

        // GET: Uczniowie/Create
        public IActionResult Create()
        {
            ViewData["Id_klasy"] = new SelectList(_context.Klasy, "Id_klasy", "NazwaKlasy");
            ViewData["Id_rodzica"] = new SelectList(_context.Rodzice, "Id_rodzica", "Imie");
            ViewData["Id_wychowawcy"] = new SelectList(_context.Nauczyciele, "Id_wychowawcy", "Imie");
            return View();
        }

        // POST: Uczniowie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Ucznia,Imie,Nazwisko,Id_rodzica,Id_klasy,Id_wychowawcy")] Uczen uczen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uczen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_klasy"] = new SelectList(_context.Klasy, "Id_klasy", "NazwaKlasy", uczen.Id_klasy);
            ViewData["Id_rodzica"] = new SelectList(_context.Rodzice, "Id_rodzica", "Imie", uczen.Id_rodzica);
            ViewData["Id_wychowawcy"] = new SelectList(_context.Nauczyciele, "Id_wychowawcy", "Imie", uczen.Id_wychowawcy);
            return View(uczen);
        }

        // GET: Uczniowie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uczen = await _context.Uczniowie.FindAsync(id);
            if (uczen == null)
            {
                return NotFound();
            }
            ViewData["Id_klasy"] = new SelectList(_context.Klasy, "Id_klasy", "NazwaKlasy", uczen.Id_klasy);
            ViewData["Id_rodzica"] = new SelectList(_context.Rodzice, "Id_rodzica", "Imie", uczen.Id_rodzica);
            ViewData["Id_wychowawcy"] = new SelectList(_context.Nauczyciele, "Id_wychowawcy", "Imie", uczen.Id_wychowawcy);
            return View(uczen);
        }

        // POST: Uczniowie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Ucznia,Imie,Nazwisko,Id_rodzica,Id_klasy,Id_wychowawcy")] Uczen uczen)
        {
            if (id != uczen.Id_Ucznia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uczen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UczenExists(uczen.Id_Ucznia))
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
            ViewData["Id_klasy"] = new SelectList(_context.Klasy, "Id_klasy", "NazwaKlasy", uczen.Id_klasy);
            ViewData["Id_rodzica"] = new SelectList(_context.Rodzice, "Id_rodzica", "Imie", uczen.Id_rodzica);
            ViewData["Id_wychowawcy"] = new SelectList(_context.Nauczyciele, "Id_wychowawcy", "Imie", uczen.Id_wychowawcy);
            return View(uczen);
        }

        // GET: Uczniowie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uczen = await _context.Uczniowie
                .Include(u => u.Klasa)
                .Include(u => u.Rodzic)
                .Include(u => u.Wychowawca)
                .FirstOrDefaultAsync(m => m.Id_Ucznia == id);
            if (uczen == null)
            {
                return NotFound();
            }

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
            return _context.Uczniowie.Any(e => e.Id_Ucznia == id);
        }
    }
}
