using Microsoft.EntityFrameworkCore;
using Dziennik_szkolny.Models;
namespace Dziennik_szkolny.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SzkolaDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        public DbSet<Uczen> Uczniowie { get; set; }
        public DbSet<Rodzic> Rodzice { get; set; }
        public DbSet<Nauczyciel> Nauczyciele { get; set; }
        public DbSet<Klasa> Klasy { get; set; }
        public DbSet<Przedmiot> Przedmioty { get; set; }
        public DbSet<Ocena> Oceny { get; set; }

        // Data/ApplicationDbContext.cs
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // WAŻNA NOWA LINIA: Wyłącza domyślne kaskadowe usuwanie
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Twoja istniejąca konfiguracja Wiele-do-Wielu:
            modelBuilder.Entity<Klasa>()
                .HasMany(k => k.Przedmioty)
                .WithMany(p => p.Klasy)
                .UsingEntity(j => j.ToTable("KlasaPrzedmiot")); 
        }
    }
}

