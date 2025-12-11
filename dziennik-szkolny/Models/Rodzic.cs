using System.ComponentModel.DataAnnotations;

namespace Dziennik_szkolny.Models
{
    public class Rodzic
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

        [Display(Name = "Uczeń")]
        public int UczenId { get; set; }
        public Uczen? Uczen { get; set; }

        [Display(Name = "Konto użytkownika")]
        public int KontoId { get; set; }
        public Konto? Konto { get; set; }
    }
}