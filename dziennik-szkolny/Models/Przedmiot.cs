
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dziennik_szkolny.Models
{
    public class Przedmiot
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nazwa przedmiotu jest wymagana")]
        [Display(Name = "Nazwa przedmiotu")]
        public string Nazwa { get; set; }
        
        [Display(Name = "Treść kształcenia")]
        public string? TrescKsztalcenia { get; set; }

        [Display(Name = "Nauczyciel")]
        public int? NauczycielId { get; set; }
        public Nauczyciel? Nauczyciel { get; set; }

        public ICollection<Klasa> Klasy { get; set; } = new List<Klasa>();
    }
}
