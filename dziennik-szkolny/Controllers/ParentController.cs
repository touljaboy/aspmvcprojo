using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dziennik_szkolny.Data;
using Dziennik_szkolny.Models;

namespace Dziennik_szkolny.Controllers
{
    [Authorize(Roles = "Rodzic")]
    public class ParentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParentController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int GetParentId()
        {
            return int.Parse(User.FindFirst("UserId")?.Value ?? "0");
        }

        public async Task<IActionResult> Dashboard()
        {
            var parentId = GetParentId();
            var parent = await _context.Rodzice
                .Include(r => r.Uczen)
                    .ThenInclude(u => u.Klasa)
                        .ThenInclude(k => k.Wychowawca)
                .Include(r => r.Uczen)
                    .ThenInclude(u => u.Klasa)
                        .ThenInclude(k => k.Przedmioty)
                .FirstOrDefaultAsync(r => r.Id == parentId);

            if (parent == null)
                return NotFound();

            // Get child's recent grades
            var recentGrades = await _context.Oceny
                .Where(o => o.UczenId == parent.UczenId)
                .Include(o => o.Przedmiot)
                .OrderByDescending(o => o.Data)
                .Take(10)
                .ToListAsync();

            ViewBag.RecentGrades = recentGrades;

            // Calculate child's average
            var allGrades = await _context.Oceny
                .Where(o => o.UczenId == parent.UczenId)
                .ToListAsync();

            ViewBag.AverageGrade = allGrades.Any() ? Math.Round(allGrades.Average(o => o.Wartosc), 2) : 0;

            // Get announcements
            var announcements = await _context.Ogloszenie
                .OrderByDescending(o => o.Data)
                .Take(5)
                .ToListAsync();

            ViewBag.Announcements = announcements;

            return View("~/Views/Rodzice/Dashboard.cshtml", parent);
        }

        public async Task<IActionResult> ChildGrades(int? przedmiotId)
        {
            var parentId = GetParentId();
            
            var parent = await _context.Rodzice
                .Include(r => r.Uczen)
                    .ThenInclude(u => u.Klasa)
                        .ThenInclude(k => k.Przedmioty)
                .FirstOrDefaultAsync(r => r.Id == parentId);

            if (parent == null)
                return NotFound();

            ViewBag.Przedmioty = parent.Uczen.Klasa.Przedmioty.ToList();
            ViewBag.ChildName = $"{parent.Uczen.Imie} {parent.Uczen.Nazwisko}";

            var gradesQuery = _context.Oceny
                .Where(o => o.UczenId == parent.UczenId)
                .Include(o => o.Przedmiot)
                .AsQueryable();

            if (przedmiotId.HasValue)
            {
                gradesQuery = gradesQuery.Where(o => o.PrzedmiotId == przedmiotId.Value);
            }

            var grades = await gradesQuery
                .OrderByDescending(o => o.Data)
                .ToListAsync();

            // Calculate statistics by subject
            var statsBySubject = grades
                .GroupBy(o => o.Przedmiot)
                .Select(g => new
                {
                    Przedmiot = g.Key,
                    Average = Math.Round(g.Average(o => o.Wartosc), 2),
                    Count = g.Count()
                })
                .ToList();

            ViewBag.StatsBySubject = statsBySubject;

            return View("~/Views/Rodzice/ChildGrades.cshtml", grades);
        }

        public async Task<IActionResult> ChildSubjects()
        {
            var parentId = GetParentId();
            
            var parent = await _context.Rodzice
                .Include(r => r.Uczen)
                    .ThenInclude(u => u.Klasa)
                        .ThenInclude(k => k.Przedmioty)
                            .ThenInclude(p => p.Nauczyciel)
                .FirstOrDefaultAsync(r => r.Id == parentId);

            if (parent == null)
                return NotFound();

            ViewBag.ChildName = $"{parent.Uczen.Imie} {parent.Uczen.Nazwisko}";

            return View("~/Views/Rodzice/ChildSubjects.cshtml", parent.Uczen.Klasa.Przedmioty.ToList());
        }

        public async Task<IActionResult> MyProfile()
        {
            var parentId = GetParentId();
            
            var parent = await _context.Rodzice
                .Include(r => r.Konto)
                .Include(r => r.Uczen)
                .FirstOrDefaultAsync(r => r.Id == parentId);

            if (parent == null)
                return NotFound();

            return View("~/Views/Rodzice/MyProfile.cshtml", parent);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var parentId = GetParentId();
            
            var parent = await _context.Rodzice
                .FirstOrDefaultAsync(r => r.Id == parentId);

            if (parent == null)
                return NotFound();

            return View("~/Views/Rodzice/EditProfile.cshtml", parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile([Bind("Id,Email,Telefon")] Rodzic rodzic)
        {
            var parentId = GetParentId();
            
            if (rodzic.Id != parentId)
                return Forbid();

            var existingParent = await _context.Rodzice.FindAsync(parentId);
            
            if (existingParent == null)
                return NotFound();

            existingParent.Email = rodzic.Email;
            existingParent.Telefon = rodzic.Telefon;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Dane kontaktowe zostaly zaktualizowane.";
                return RedirectToAction(nameof(MyProfile));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Wystapil blad podczas zapisywania zmian.");
            }

            return View("~/Views/Rodzice/EditProfile.cshtml", existingParent);
        }
    }
}
