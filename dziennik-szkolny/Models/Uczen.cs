using System.ComponentModel.DataAnnotations;

namespace Dziennik_szkolny.Models
{
    public class Uczen
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Imię jest wymagane")]
        [Display(Name = "Imię")]
        public string Imie { get; set; }
        
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; }
        [EmailAddress(ErrorMessage = "Nieprawidłowy format email")]
        public string? Email { get; set; }
        
        [Phone(ErrorMessage = "Nieprawidłowy format telefonu")]
        public string? Telefon { get; set; }

        [Display(Name = "Klasa")]
        public int KlasaId { get; set; }
        public Klasa? Klasa { get; set; }

        [Display(Name = "Konto użytkownika")]
        public int KontoId { get; set; }
        public Konto? Konto { get; set; }

        public ICollection<Ocena> Oceny { get; set; } = new List<Ocena>();
        public ICollection<Rodzic> Rodzice { get; set; } = new List<Rodzic>();
    }
}