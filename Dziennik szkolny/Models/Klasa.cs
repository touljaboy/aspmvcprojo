
using System.ComponentModel.DataAnnotations;
namespace Dziennik_szkolny.Models
{
    public class Klasa
    {
        [Key]
        public int Id_klasy { get; set; } 
        
        [Required]
        public string NazwaKlasy { get; set; } // np. "4A"


        public ICollection<Uczen> Uczniowie { get; set; } = new List<Uczen>();

        public ICollection<Przedmiot> Przedmioty { get; set; } = new List<Przedmiot>();
    }  
}
