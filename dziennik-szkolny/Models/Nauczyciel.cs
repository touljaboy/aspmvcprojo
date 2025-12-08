using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_szkolny.Models
{
    public class Nauczyciel
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public bool CzyWychowawca { get; set; }

        public int KontoId { get; set; }
        public Konto Konto { get; set; }

        public int? PrzelozonyId { get; set; }
        [ForeignKey("PrzelozonyId")]
        public Nauczyciel? Przelozony { get; set; }
    }
}