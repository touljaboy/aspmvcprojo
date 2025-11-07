// Models/Klasa.cs
using System.ComponentModel.DataAnnotations;
namespace Dziennik_szkolny.Models
{
    public class Klasa
    {
        [Key]
        public int Id_klasy { get; set; } 
        
        [Required]
        public string NazwaKlasy { get; set; } // np. "4A"

        // Relacje:

        // Właściwość nawigacyjna: Klasa ma wielu Uczniów
        public ICollection<Uczen> Uczniowie { get; set; } = new List<Uczen>();

        // Klasa ma wiele Przedmiotów (relacja Wiele do Wielu, ale na poziomie logiki bazy to jest inna relacja)
        public ICollection<Przedmiot> Przedmioty { get; set; } = new List<Przedmiot>();
    }  
}
