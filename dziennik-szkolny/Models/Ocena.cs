
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dziennik_szkolny.Models
{
        public class Ocena
    {
        [Key]
        public int Id_oceny { get; set; }
        
        [Required]
        public int Wartosc { get; set; } // np. 5, 4, 3
        
        public DateTime DataWpisu { get; set; } = DateTime.Now;

        public int Id_Ucznia { get; set; }
        [ForeignKey("Id_Ucznia")]
        public Uczen Uczen { get; set; }

        public int Id_przedmiotu { get; set; }
        [ForeignKey("Id_przedmiotu")]
        public Przedmiot Przedmiot { get; set; }


        public int Id_nauczyciela { get; set; }
        [ForeignKey("Id_nauczyciela")]
        public Nauczyciel Nauczyciel { get; set; }
    }
}
