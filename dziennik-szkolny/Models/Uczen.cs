
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dziennik_szkolny.Models
{
    public class Uczen
    {
        [Key]
        public int Id_Ucznia { get; set; } 
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        
        public int Id_rodzica { get; set; } 
        [ForeignKey("Id_rodzica")]
        public Rodzic Rodzic { get; set; } 

        public int Id_klasy { get; set; }
        [ForeignKey("Id_klasy")]
        public Klasa Klasa { get; set; } 

        public int Id_wychowawcy { get; set; }
        [ForeignKey("Id_wychowawcy")]
        public Nauczyciel Wychowawca { get; set; } 
        
        public ICollection<Ocena> Oceny { get; set; } = new List<Ocena>();
    }  
}
