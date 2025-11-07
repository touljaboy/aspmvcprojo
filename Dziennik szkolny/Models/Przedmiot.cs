
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

        public int Id_nauczyciela { get; set; }
        [ForeignKey("Id_nauczyciela")]
        public Nauczyciel Nauczyciel { get; set; } 

        public ICollection<Ocena> Oceny { get; set; } = new List<Ocena>();

        public ICollection<Klasa> Klasy { get; set; } = new List<Klasa>();
    }
}
