using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Dziennik_szkolny.Data;

namespace Dziennik_szkolny.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool success, ClaimsPrincipal? principal, string? role, int? userId)> AuthenticateAsync(string username, string password)
        {
            var konto = await _context.Konto
                .FirstOrDefaultAsync(k => k.Nazwa == username && k.HasloHash == password); // In production, use proper password hashing!

            if (konto == null)
                return (false, null, null, null);

            // Find the actual user based on role
            int? actualUserId = null;
            if (konto.Rola == "Nauczyciel")
            {
                var nauczyciel = await _context.Nauczyciele.FirstOrDefaultAsync(n => n.KontoId == konto.Id);
                actualUserId = nauczyciel?.Id;
            }
            else if (konto.Rola == "Uczen")
            {
                var uczen = await _context.Uczniowie.FirstOrDefaultAsync(u => u.KontoId == konto.Id);
                actualUserId = uczen?.Id;
            }
            else if (konto.Rola == "Rodzic")
            {
                var rodzic = await _context.Rodzice.FirstOrDefaultAsync(r => r.KontoId == konto.Id);
                actualUserId = rodzic?.Id;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, konto.Nazwa),
                new Claim(ClaimTypes.Role, konto.Rola),
                new Claim("KontoId", konto.Id.ToString()),
                new Claim("UserId", actualUserId?.ToString() ?? "0")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            return (true, principal, konto.Rola, actualUserId);
        }
    }
}
