using System;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_szkolny.Models
{
    public class Ocena
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Wartość oceny jest wymagana")]
        [Range(1, 6, ErrorMessage = "Ocena musi być w zakresie 1-6")]
        [Display(Name = "Ocena")]
        public int Wartosc { get; set; }
        
        [Required(ErrorMessage = "Opis jest wymagany")]
        [Display(Name = "Opis")]
        public string Opis { get; set; }
        
        [Display(Name = "Data")]
        public DateTime Data { get; set; } = DateTime.Now;

        [Display(Name = "Uczeń")]
        public int UczenId { get; set; }
        public Uczen? Uczen { get; set; }

        [Display(Name = "Przedmiot")]
        public int PrzedmiotId { get; set; }
        public Przedmiot? Przedmiot { get; set; }
    }
}