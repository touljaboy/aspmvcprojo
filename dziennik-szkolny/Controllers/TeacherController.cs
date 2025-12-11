using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dziennik_szkolny.Data;
using Dziennik_szkolny.Models;
using System.Security.Claims;

namespace Dziennik_szkolny.Controllers
{
    [Authorize(Roles = "Nauczyciel")]
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int GetTeacherId()
        {
            return int.Parse(User.FindFirst("UserId")?.Value ?? "0");
        }

        public async Task<IActionResult> Dashboard()
        {
            var teacherId = GetTeacherId();
            var teacher = await _context.Nauczyciele
                .Include(n => n.Przedmioty)
                    .ThenInclude(p => p.Klasy)
                .FirstOrDefaultAsync(n => n.Id == teacherId);

            if (teacher == null)
                return NotFound();

            // Get classes where teacher is supervisor
            var supervisedClasses = await _context.Klasy
                .Where(k => k.WychowawcaId == teacherId)
                .Include(k => k.Uczniowie)
                .ToListAsync();

            ViewBag.SupervisedClasses = supervisedClasses;
            
            // Get recent announcements
            var announcements = await _context.Ogloszenie
                .OrderByDescending(o => o.Data)
                .Take(5)
                .ToListAsync();
            
            ViewBag.Announcements = announcements;

            return View("~/Views/Nauczyciele/Dashboard.cshtml", teacher);
        }

        // View my students
        public async Task<IActionResult> MyStudents()
        {
            var teacherId = GetTeacherId();
            
            // Get all classes where teacher teaches
            var przedmioty = await _context.Przedmioty
                .Where(p => p.NauczycielId == teacherId)
                .Include(p => p.Klasy)
                    .ThenInclude(k => k.Uczniowie)
                .ToListAsync();

            var students = przedmioty
                .SelectMany(p => p.Klasy)
                .SelectMany(k => k.Uczniowie)
                .Distinct()
                .OrderBy(u => u.Nazwisko)
                .ToList();

            return View("~/Views/Nauczyciele/MyStudents.cshtml", students);
        }

        // Add grade
        [HttpGet]
        public async Task<IActionResult> AddGrade(int? studentId)
        {
            var teacherId = GetTeacherId();
            
            // Get subjects taught by this teacher
            var subjects = await _context.Przedmioty
                .Where(p => p.NauczycielId == teacherId)
                .ToListAsync();

            ViewData["PrzedmiotId"] = new SelectList(subjects, "Id", "Nazwa");

            // Get students for the subjects this teacher teaches
            var students = await _context.Uczniowie
                .Where(u => u.Klasa.Przedmioty.Any(p => p.NauczycielId == teacherId))
                .Select(u => new { u.Id, Display = u.Imie + " " + u.Nazwisko + " - " + u.Klasa.NazwaKlasy })
                .ToListAsync();

            ViewData["UczenId"] = new SelectList(students, "Id", "Display", studentId);

            return View("~/Views/Nauczyciele/AddGrade.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGrade([Bind("Id,UczenId,PrzedmiotId,Wartosc,Opis,Data")] Ocena ocena)
        {
            var teacherId = GetTeacherId();
            
            // Verify teacher teaches this subject
            var subject = await _context.Przedmioty
                .FirstOrDefaultAsync(p => p.Id == ocena.PrzedmiotId && p.NauczycielId == teacherId);

            if (subject == null)
            {
                ModelState.AddModelError("", "Nie masz uprawnień do dodawania ocen z tego przedmiotu");
            }

            if (ModelState.IsValid)
            {
                _context.Add(ocena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MyStudents));
            }

            var subjects = await _context.Przedmioty
                .Where(p => p.NauczycielId == teacherId)
                .ToListAsync();
            ViewData["PrzedmiotId"] = new SelectList(subjects, "Id", "Nazwa", ocena.PrzedmiotId);

            var students = await _context.Uczniowie
                .Where(u => u.Klasa.Przedmioty.Any(p => p.NauczycielId == teacherId))
                .Select(u => new { u.Id, Display = u.Imie + " " + u.Nazwisko + " - " + u.Klasa.NazwaKlasy })
                .ToListAsync();
            ViewData["UczenId"] = new SelectList(students, "Id", "Display", ocena.UczenId);

            return View("~/Views/Nauczyciele/AddGrade.cshtml", ocena);
        }

        // View grades for my classes
        public async Task<IActionResult> ViewGrades(int? klasaId, int? przedmiotId)
        {
            var teacherId = GetTeacherId();

            // Get subjects taught by this teacher
            var subjects = await _context.Przedmioty
                .Where(p => p.NauczycielId == teacherId)
                .ToListAsync();

            ViewData["PrzedmiotId"] = new SelectList(subjects, "Id", "Nazwa");

            // Get classes where teacher teaches
            var classes = await _context.Klasy
                .Where(k => k.Przedmioty.Any(p => p.NauczycielId == teacherId))
                .ToListAsync();

            ViewData["KlasaId"] = new SelectList(classes, "Id", "NazwaKlasy");

            var gradesQuery = _context.Oceny
                .Include(o => o.Uczen)
                    .ThenInclude(u => u.Klasa)
                .Include(o => o.Przedmiot)
                .Where(o => o.Przedmiot.NauczycielId == teacherId)
                .AsQueryable();

            if (klasaId.HasValue)
            {
                gradesQuery = gradesQuery.Where(o => o.Uczen.KlasaId == klasaId.Value);
            }

            if (przedmiotId.HasValue)
            {
                gradesQuery = gradesQuery.Where(o => o.PrzedmiotId == przedmiotId.Value);
            }

            var grades = await gradesQuery
                .OrderByDescending(o => o.Data)
                .ToListAsync();

            return View("~/Views/Nauczyciele/ViewGrades.cshtml", grades);
        }

        public async Task<IActionResult> MyProfile()
        {
            var teacherId = GetTeacherId();
            
            var teacher = await _context.Nauczyciele
                .Include(n => n.Konto)
                .Include(n => n.Przelozony)
                .FirstOrDefaultAsync(n => n.Id == teacherId);

            if (teacher == null)
                return NotFound();

            return View("~/Views/Nauczyciele/MyProfile.cshtml", teacher);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var teacherId = GetTeacherId();
            
            var teacher = await _context.Nauczyciele
                .FirstOrDefaultAsync(n => n.Id == teacherId);

            if (teacher == null)
                return NotFound();

            return View("~/Views/Nauczyciele/EditProfile.cshtml", teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile([Bind("Id,Email,Telefon")] Nauczyciel nauczyciel)
        {
            var teacherId = GetTeacherId();
            
            if (nauczyciel.Id != teacherId)
                return Forbid();

            var existingTeacher = await _context.Nauczyciele.FindAsync(teacherId);
            
            if (existingTeacher == null)
                return NotFound();

            existingTeacher.Email = nauczyciel.Email;
            existingTeacher.Telefon = nauczyciel.Telefon;

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

            return View("~/Views/Nauczyciele/EditProfile.cshtml", existingTeacher);
        }
    }
}
