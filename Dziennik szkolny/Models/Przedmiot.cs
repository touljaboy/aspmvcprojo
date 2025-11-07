// Models/Przedmiot.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dziennik_szkolny.Models
{
    public class Przedmiot
    {
        [Key]
        public int Id_przedmiotu { get; set; } 
        
        [Required]
        public string NazwaPrzedmiotu { get; set; }

        // Klucz Obcy do Nauczyciela (w Twoim diagramie Nauczyciel jest powiązany z Przedmiotem)
        public int Id_nauczyciela { get; set; }
        [ForeignKey("Id_nauczyciela")]
        public Nauczyciel Nauczyciel { get; set; } // Właściwość nawigacyjna

        // Właściwość nawigacyjna: Przedmiot ma wiele Ocen
        public ICollection<Ocena> Oceny { get; set; } = new List<Ocena>();

        // Relacja Wiele do Wiele z Klasą (jedna klasa ma wiele przedmiotów, jeden przedmiot jest w wielu klasach)
        public ICollection<Klasa> Klasy { get; set; } = new List<Klasa>();
    }
}
