namespace Dziennik_szkolny.Models
{
    public class Rodzic
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public int UczenId { get; set; }
        public Uczen Uczen { get; set; }

        public int KontoId { get; set; }
        public Konto Konto { get; set; }
    }
}