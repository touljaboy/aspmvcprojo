// Models/Nauczyciel.cs
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
        
        // Właściwość nawigacyjna: Nauczyciel może być wychowawcą wielu Uczniów
        public ICollection<Uczen> Wychowankowie { get; set; } = new List<Uczen>();
        
        // Właściwość nawigacyjna: Nauczyciel prowadzi wiele Przedmiotów
        public ICollection<Przedmiot> ProwadzonePrzedmioty { get; set; } = new List<Przedmiot>();
    }
}
