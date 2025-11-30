using System;

namespace Dziennik_szkolny.Models
{
    public class Ocena
    {
        public int Id { get; set; }
        public int Wartosc { get; set; }
        public string Opis { get; set; } 
        public DateTime Data { get; set; } = DateTime.Now;

        public int UczenId { get; set; }
        public Uczen Uczen { get; set; }

        public int PrzedmiotId { get; set; }
        public Przedmiot Przedmiot { get; set; }
    }
}