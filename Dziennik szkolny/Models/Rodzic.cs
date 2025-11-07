

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace Dziennik_szkolny.Models
{
    public class Rodzic
    {
        [Key]
        public int Id_rodzica { get; set; } 
        
        [Required]
        public string Imie { get; set; }
        
        [Required]
        public string Nazwisko { get; set; }
        
        public ICollection<Uczen> Uczniowie { get; set; } = new List<Uczen>();
    }  
}
