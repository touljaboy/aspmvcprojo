using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dziennik_szkolny.Data;
using Dziennik_szkolny.Models;

namespace Dziennik_szkolny.Controllers
{
    [Authorize(Roles = "Uczen")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int GetStudentId()
        {
            return int.Parse(User.FindFirst("UserId")?.Value ?? "0");
        }

        public async Task<IActionResult> Dashboard()
        {
            var studentId = GetStudentId();
            var student = await _context.Uczniowie
                .Include(u => u.Klasa)
                    .ThenInclude(k => k.Wychowawca)
                .Include(u => u.Klasa)
                    .ThenInclude(k => k.Przedmioty)
                .FirstOrDefaultAsync(u => u.Id == studentId);

            if (student == null)
                return NotFound();

            // Get recent grades
            var recentGrades = await _context.Oceny
                .Where(o => o.UczenId == studentId)
                .Include(o => o.Przedmiot)
                .OrderByDescending(o => o.Data)
                .Take(10)
                .ToListAsync();

            ViewBag.RecentGrades = recentGrades;

            // Calculate average
            var allGrades = await _context.Oceny
                .Where(o => o.UczenId == studentId)
                .ToListAsync();

            ViewBag.AverageGrade = allGrades.Any() ? Math.Round(allGrades.Average(o => o.Wartosc), 2) : 0;

            // Get announcements
            var announcements = await _context.Ogloszenie
                .OrderByDescending(o => o.Data)
                .Take(5)
                .ToListAsync();

            ViewBag.Announcements = announcements;

            return View("~/Views/Uczniowie/Dashboard.cshtml", student);
        }

        public async Task<IActionResult> MyGrades(int? przedmiotId)
        {
            var studentId = GetStudentId();
            
            var student = await _context.Uczniowie
                .Include(u => u.Klasa)
                    .ThenInclude(k => k.Przedmioty)
                .FirstOrDefaultAsync(u => u.Id == studentId);

            if (student == null)
                return NotFound();

            ViewBag.Przedmioty = student.Klasa.Przedmioty.ToList();

            var gradesQuery = _context.Oceny
                .Where(o => o.UczenId == studentId)
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

            return View("~/Views/Uczniowie/MyGrades.cshtml", grades);
        }

        public async Task<IActionResult> MySubjects()
        {
            var studentId = GetStudentId();
            
            var student = await _context.Uczniowie
                .Include(u => u.Klasa)
                    .ThenInclude(k => k.Przedmioty)
                        .ThenInclude(p => p.Nauczyciel)
                .FirstOrDefaultAsync(u => u.Id == studentId);

            if (student == null)
                return NotFound();

            return View("~/Views/Uczniowie/MySubjects.cshtml", student.Klasa.Przedmioty.ToList());
        }

        public async Task<IActionResult> MyProfile()
        {
            var studentId = GetStudentId();
            
            var student = await _context.Uczniowie
                .Include(u => u.Klasa)
                .Include(u => u.Konto)
                .FirstOrDefaultAsync(u => u.Id == studentId);

            if (student == null)
                return NotFound();

            return View("~/Views/Uczniowie/MyProfile.cshtml", student);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var studentId = GetStudentId();
            
            var student = await _context.Uczniowie
                .FirstOrDefaultAsync(u => u.Id == studentId);

            if (student == null)
                return NotFound();

            return View("~/Views/Uczniowie/EditProfile.cshtml", student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile([Bind("Id,Email,Telefon")] Uczen uczen)
        {
            var studentId = GetStudentId();
            
            if (uczen.Id != studentId)
                return Forbid();

            // Get existing student from database
            var existingStudent = await _context.Uczniowie.FindAsync(studentId);
            
            if (existingStudent == null)
                return NotFound();

            // Only update email and phone
            existingStudent.Email = uczen.Email;
            existingStudent.Telefon = uczen.Telefon;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Dane kontaktowe zostały zaktualizowane.";
                return RedirectToAction(nameof(MyProfile));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas zapisywania zmian.");
            }

            return View("~/Views/Uczniowie/EditProfile.cshtml", existingStudent);
        }
    }
}
