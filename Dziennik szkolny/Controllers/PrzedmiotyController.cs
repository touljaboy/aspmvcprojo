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
    public class PrzedmiotyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrzedmiotyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Przedmioty
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Przedmioty.Include(p => p.Nauczyciel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Przedmioty/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przedmiot = await _context.Przedmioty
                .Include(p => p.Nauczyciel)
                .FirstOrDefaultAsync(m => m.Id_przedmiotu == id);
            if (przedmiot == null)
            {
                return NotFound();
            }

            return View(przedmiot);
        }

        // GET: Przedmioty/Create
        public IActionResult Create()
        {
            ViewData["Id_nauczyciela"] = new SelectList(_context.Nauczyciele, "Id_wychowawcy", "Imie");
            return View();
        }

        // POST: Przedmioty/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_przedmiotu,NazwaPrzedmiotu,Id_nauczyciela")] Przedmiot przedmiot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(przedmiot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_nauczyciela"] = new SelectList(_context.Nauczyciele, "Id_wychowawcy", "Imie", przedmiot.Id_nauczyciela);
            return View(przedmiot);
        }

        // GET: Przedmioty/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przedmiot = await _context.Przedmioty.FindAsync(id);
            if (przedmiot == null)
            {
                return NotFound();
            }
            ViewData["Id_nauczyciela"] = new SelectList(_context.Nauczyciele, "Id_wychowawcy", "Imie", przedmiot.Id_nauczyciela);
            return View(przedmiot);
        }

        // POST: Przedmioty/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_przedmiotu,NazwaPrzedmiotu,Id_nauczyciela")] Przedmiot przedmiot)
        {
            if (id != przedmiot.Id_przedmiotu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(przedmiot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzedmiotExists(przedmiot.Id_przedmiotu))
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
            ViewData["Id_nauczyciela"] = new SelectList(_context.Nauczyciele, "Id_wychowawcy", "Imie", przedmiot.Id_nauczyciela);
            return View(przedmiot);
        }

        // GET: Przedmioty/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przedmiot = await _context.Przedmioty
                .Include(p => p.Nauczyciel)
                .FirstOrDefaultAsync(m => m.Id_przedmiotu == id);
            if (przedmiot == null)
            {
                return NotFound();
            }

            return View(przedmiot);
        }

        // POST: Przedmioty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var przedmiot = await _context.Przedmioty.FindAsync(id);
            if (przedmiot != null)
            {
                _context.Przedmioty.Remove(przedmiot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzedmiotExists(int id)
        {
            return _context.Przedmioty.Any(e => e.Id_przedmiotu == id);
        }
    }
}
