using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Dziennik_szkolny.Services;

namespace Dziennik_szkolny.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
        {
            var (success, principal, role, userId) = await _authService.AuthenticateAsync(username, password);

            if (!success || principal == null)
            {
                ViewBag.Error = "Nieprawidłowa nazwa użytkownika lub hasło";
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Redirect based on role
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return role switch
            {
                "Admin" => RedirectToAction("Index", "Home"),
                "Nauczyciel" => RedirectToAction("Dashboard", "Teacher"),
                "Uczen" => RedirectToAction("Dashboard", "Student"),
                "Rodzic" => RedirectToAction("Dashboard", "Parent"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
