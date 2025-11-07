// Models/Rodzic.cs

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

// Pamiętaj, aby umieścić to w prawidłowej przestrzeni nazw Twojego projektu!
// Na przykład:
namespace Dziennik_szkolny.Models
{
    public class Rodzic
    {
        // Klucz główny
        [Key]
        public int Id_rodzica { get; set; } 
        
        [Required]
        public string Imie { get; set; }
        
        [Required]
        public string Nazwisko { get; set; }
        
        // Właściwość nawigacyjna: Rodzic może mieć wielu Uczniów (relacja Jeden-do-Wielu)
        public ICollection<Uczen> Uczniowie { get; set; } = new List<Uczen>();
    }  
}
