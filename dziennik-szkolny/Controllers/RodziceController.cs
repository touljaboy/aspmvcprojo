using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dziennik_szkolny.Data;
using Dziennik_szkolny.Models;

namespace dziennik_szkolny.Controllers
{
    public class RodziceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RodziceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rodzice
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rodzice.Include(r => r.Konto).Include(r => r.Uczen);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rodzice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rodzic = await _context.Rodzice
                .Include(r => r.Konto)
                .Include(r => r.Uczen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rodzic == null)
            {
                return NotFound();
            }

            return View(rodzic);
        }

        // GET: Rodzice/Create
        public IActionResult Create()
        {
            ViewData["KontoId"] = new SelectList(_context.Konto, "Id", "Id");
            ViewData["UczenId"] = new SelectList(_context.Uczniowie, "Id", "Id");
            return View();
        }

        // POST: Rodzice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,UczenId,KontoId")] Rodzic rodzic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rodzic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KontoId"] = new SelectList(_context.Konto, "Id", "Id", rodzic.KontoId);
            ViewData["UczenId"] = new SelectList(_context.Uczniowie, "Id", "Id", rodzic.UczenId);
            return View(rodzic);
        }

        // GET: Rodzice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rodzic = await _context.Rodzice.FindAsync(id);
            if (rodzic == null)
            {
                return NotFound();
            }
            ViewData["KontoId"] = new SelectList(_context.Konto, "Id", "Id", rodzic.KontoId);
            ViewData["UczenId"] = new SelectList(_context.Uczniowie, "Id", "Id", rodzic.UczenId);
            return View(rodzic);
        }

        // POST: Rodzice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,UczenId,KontoId")] Rodzic rodzic)
        {
            if (id != rodzic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rodzic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RodzicExists(rodzic.Id))
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
            ViewData["KontoId"] = new SelectList(_context.Konto, "Id", "Id", rodzic.KontoId);
            ViewData["UczenId"] = new SelectList(_context.Uczniowie, "Id", "Id", rodzic.UczenId);
            return View(rodzic);
        }

        // GET: Rodzice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rodzic = await _context.Rodzice
                .Include(r => r.Konto)
                .Include(r => r.Uczen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rodzic == null)
            {
                return NotFound();
            }

            return View(rodzic);
        }

        // POST: Rodzice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rodzic = await _context.Rodzice.FindAsync(id);
            if (rodzic != null)
            {
                _context.Rodzice.Remove(rodzic);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RodzicExists(int id)
        {
            return _context.Rodzice.Any(e => e.Id == id);
        }
    }
}
