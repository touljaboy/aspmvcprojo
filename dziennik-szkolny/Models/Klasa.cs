
using System.ComponentModel.DataAnnotations;
namespace Dziennik_szkolny.Models
{
    public class Klasa
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nazwa klasy jest wymagana")]
        [Display(Name = "Nazwa klasy")]
        public string NazwaKlasy { get; set; }

        [Display(Name = "Wychowawca")]
        public int? WychowawcaId { get; set; }
        public Nauczyciel? Wychowawca { get; set; }

        public ICollection<Przedmiot> Przedmioty { get; set; } = new List<Przedmiot>();

        public ICollection<Uczen> Uczniowie { get; set; } = new List<Uczen>();
    }
}
