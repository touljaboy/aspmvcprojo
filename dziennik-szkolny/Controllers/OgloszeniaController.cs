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
    public class OgloszeniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OgloszeniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ogloszenia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ogloszenie.ToListAsync());
        }

        // GET: Ogloszenia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ogloszenie == null)
            {
                return NotFound();
            }

            return View(ogloszenie);
        }

        // GET: Ogloszenia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ogloszenia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tytul,Tresc,Data")] Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ogloszenie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ogloszenie);
        }

        // GET: Ogloszenia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenie.FindAsync(id);
            if (ogloszenie == null)
            {
                return NotFound();
            }
            return View(ogloszenie);
        }

        // POST: Ogloszenia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tytul,Tresc,Data")] Ogloszenie ogloszenie)
        {
            if (id != ogloszenie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ogloszenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgloszenieExists(ogloszenie.Id))
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
            return View(ogloszenie);
        }

        // GET: Ogloszenia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ogloszenie == null)
            {
                return NotFound();
            }

            return View(ogloszenie);
        }

        // POST: Ogloszenia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ogloszenie = await _context.Ogloszenie.FindAsync(id);
            if (ogloszenie != null)
            {
                _context.Ogloszenie.Remove(ogloszenie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OgloszenieExists(int id)
        {
            return _context.Ogloszenie.Any(e => e.Id == id);
        }
    }
}
