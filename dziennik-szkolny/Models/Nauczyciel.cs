using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_szkolny.Models
{
    public class Nauczyciel
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
        
        [Display(Name = "Czy wychowawca")]
        public bool CzyWychowawca { get; set; }

        [Display(Name = "Konto użytkownika")]
        public int KontoId { get; set; }
        public Konto? Konto { get; set; }

        [Display(Name = "Przełożony")]
        public int? PrzelozonyId { get; set; }
        [ForeignKey("PrzelozonyId")]
        public Nauczyciel Przelozony { get; set; }

        public ICollection<Przedmiot> Przedmioty { get; set; } = new List<Przedmiot>();
    }
}