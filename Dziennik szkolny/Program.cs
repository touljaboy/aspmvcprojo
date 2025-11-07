using Microsoft.EntityFrameworkCore;
using Dziennik_szkolny.Data; // Wymagane, by znaleźć ApplicationDbContext
using Microsoft.AspNetCore.Hosting; // Dla UseStaticAssets (jeśli używasz)

var builder = WebApplication.CreateBuilder(args);

// --- KONFIGURACJA BAZY DANYCH I USŁUG (Dodano tutaj) ---

// 1. Pobranie ciągu połączenia z appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Rejestracja ApplicationDbContext jako usługi (używając SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Dodanie kontrolerów MVC do usług.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// --- KONFIGURACJA POTOKU ŻĄDAŃ ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Domyślny HSTS
    app.UseHsts();
}

app.UseHttpsRedirection();

// Domyślnie użyjemy UseStaticFiles
app.UseStaticFiles(); 

app.UseRouting();

app.UseAuthorization();

// app.MapStaticAssets(); // Ta linia jest niepotrzebna, jeśli używasz UseStaticFiles

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    // .WithStaticAssets(); // Ta linia jest niepotrzebna

app.Run();