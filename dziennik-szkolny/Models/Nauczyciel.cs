
using System.ComponentModel.DataAnnotations;
namespace Dziennik_szkolny.Models
{
    public class Nauczyciel
    {
        [Key]
        public int Id_wychowawcy { get; set; } 
        
        [Required]
        public string Imie { get; set; }
        
        [Required]
        public string Nazwisko { get; set; }
        

        public ICollection<Uczen> Wychowankowie { get; set; } = new List<Uczen>();

        public ICollection<Przedmiot> ProwadzonePrzedmioty { get; set; } = new List<Przedmiot>();
    }
}
