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
            return View(await _context.Konto.ToListAsync());
        }

        // GET: Konta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konto = await _context.Konto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (konto == null)
            {
                return NotFound();
            }

            return View(konto);
        }

        // GET: Konta/Create
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(new[] { "Admin", "Nauczyciel", "Uczen", "Rodzic" });
            return View();
        }

        // POST: Konta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,HasloHash,Rola")] Konto konto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(konto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Roles = new SelectList(new[] { "Admin", "Nauczyciel", "Uczen", "Rodzic" }, konto.Rola);
            return View(konto);
        }

        // GET: Konta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konto = await _context.Konto.FindAsync(id);
            if (konto == null)
            {
                return NotFound();
            }
            ViewBag.Roles = new SelectList(new[] { "Admin", "Nauczyciel", "Uczen", "Rodzic" }, konto.Rola);
            return View(konto);
        }

        // POST: Konta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,HasloHash,Rola")] Konto konto)
        {
            if (id != konto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(konto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KontoExists(konto.Id))
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
            ViewBag.Roles = new SelectList(new[] { "Admin", "Nauczyciel", "Uczen", "Rodzic" }, konto.Rola);
            return View(konto);
        }

        // GET: Konta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konto = await _context.Konto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (konto == null)
            {
                return NotFound();
            }

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
