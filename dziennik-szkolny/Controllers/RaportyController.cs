using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dziennik_szkolny.Data;
using Dziennik_szkolny.Models;

namespace dziennik_szkolny.Controllers
{
    [Authorize(Roles = "Admin,Nauczyciel")]
    public class RaportyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RaportyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Raporty
        public async Task<IActionResult> Index(int? uczenId, int? przedmiotId, DateTime? dataOd, DateTime? dataDo)
        {
            ViewData["UczenId"] = new SelectList(_context.Uczniowie.Select(u => new 
            { 
                Id = u.Id, 
                Display = u.Imie + " " + u.Nazwisko 
            }), "Id", "Display");
            
            ViewData["PrzedmiotId"] = new SelectList(_context.Przedmioty, "Id", "Nazwa");

            var ocenyQuery = _context.Oceny
                .Include(o => o.Uczen)
                    .ThenInclude(u => u.Klasa)
                .Include(o => o.Przedmiot)
                .AsQueryable();

            // Filtrowanie po uczniu
            if (uczenId.HasValue)
            {
                ocenyQuery = ocenyQuery.Where(o => o.UczenId == uczenId.Value);
            }

            // Filtrowanie po przedmiocie
            if (przedmiotId.HasValue)
            {
                ocenyQuery = ocenyQuery.Where(o => o.PrzedmiotId == przedmiotId.Value);
            }

            // Filtrowanie po dacie od
            if (dataOd.HasValue)
            {
                ocenyQuery = ocenyQuery.Where(o => o.Data >= dataOd.Value);
            }

            // Filtrowanie po dacie do
            if (dataDo.HasValue)
            {
                ocenyQuery = ocenyQuery.Where(o => o.Data <= dataDo.Value);
            }

            var oceny = await ocenyQuery.OrderByDescending(o => o.Data).ToListAsync();

            // Obliczanie statystyk
            if (oceny.Any())
            {
                ViewData["SredniaOcen"] = Math.Round(oceny.Average(o => o.Wartosc), 2);
                ViewData["LiczbaOcen"] = oceny.Count;
            }

            return View(oceny);
        }

        // GET: Raporty/Uczniowie
        public async Task<IActionResult> Uczniowie(int? klasaId)
        {
            ViewData["KlasaId"] = new SelectList(_context.Klasy, "Id", "NazwaKlasy");

            var uczniowieQuery = _context.Uczniowie
                .Include(u => u.Klasa)
                .Include(u => u.Oceny)
                    .ThenInclude(o => o.Przedmiot)
                .AsQueryable();

            if (klasaId.HasValue)
            {
                uczniowieQuery = uczniowieQuery.Where(u => u.KlasaId == klasaId.Value);
            }

            var uczniowie = await uczniowieQuery.ToListAsync();

            // Przygotowanie danych statystycznych dla każdego ucznia
            var uczniowieStatystyki = uczniowie.Select(u => new
            {
                Uczen = u,
                SredniaOcen = u.Oceny.Any() ? Math.Round(u.Oceny.Average(o => o.Wartosc), 2) : 0,
                LiczbaOcen = u.Oceny.Count
            }).ToList();

            return View(uczniowieStatystyki);
        }

        // GET: Raporty/Przedmioty
        public async Task<IActionResult> Przedmioty()
        {
            var przedmioty = await _context.Przedmioty
                .Include(p => p.Nauczyciel)
                .Include(p => p.Klasy)
                .ToListAsync();

            var przedmiotyStatystyki = przedmioty.Select(p => new
            {
                Przedmiot = p,
                LiczbaKlas = p.Klasy.Count,
                LiczbaOcen = _context.Oceny.Count(o => o.PrzedmiotId == p.Id),
                SredniaOcen = _context.Oceny.Where(o => o.PrzedmiotId == p.Id).Any() 
                    ? Math.Round(_context.Oceny.Where(o => o.PrzedmiotId == p.Id).Average(o => o.Wartosc), 2) 
                    : 0
            }).ToList();

            return View(przedmiotyStatystyki);
        }
    }
}
