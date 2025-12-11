using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dziennik_szkolny.Models;
using Dziennik_szkolny.Data;

namespace Dziennik_szkolny.Controllers;

[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var resourceLinks = new List<string>
        {
            "Konta",
            "Ogloszenia",
            "Uczniowie", 
            "Rodzice",
            "Nauczyciele",
            "Klasy",
            "Przedmioty",
            "Oceny",
            "Raporty"
        };

        ViewBag.ResourceLinks = resourceLinks;
        
        // Pobierz ostatnie 5 ogłoszeń
        var ogloszenia = await _context.Ogloszenie
            .OrderByDescending(o => o.Data)
            .Take(5)
            .ToListAsync();
        
        ViewBag.Ogloszenia = ogloszenia;
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
