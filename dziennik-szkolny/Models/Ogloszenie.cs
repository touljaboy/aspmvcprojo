using System;

namespace Dziennik_szkolny.Models
{
    public class Ogloszenie
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public string Tresc { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
    }
}