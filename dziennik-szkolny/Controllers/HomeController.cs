using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dziennik_szkolny.Models;

namespace Dziennik_szkolny.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var resourceLinks = new List<string>
        {
            "Uczniowie", 
            "Rodzice",
            "Nauczyciele",
            "Klasy",
            "Przedmioty",
            "Oceny"
        };

        ViewBag.ResourceLinks = resourceLinks;
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
