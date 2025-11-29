namespace Dziennik_szkolny.Models
{
    public class Uczen
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public int KlasaId { get; set; }
        public Klasa Klasa { get; set; }

        public int KontoId { get; set; }
        public Konto Konto { get; set; }

        public ICollection<Ocena> Oceny { get; set; } = new List<Ocena>();
    }
}