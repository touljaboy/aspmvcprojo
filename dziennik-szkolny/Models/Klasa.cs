
using System.ComponentModel.DataAnnotations;
namespace Dziennik_szkolny.Models
{
    public class Klasa
    {
        public int Id { get; set; }
        public string NazwaKlasy { get; set; }

        public int? WychowawcaId { get; set; }
        public Nauczyciel Wychowawca { get; set; }

        public ICollection<Przedmiot> Przedmioty { get; set; } = new List<Przedmiot>();

        public ICollection<Uczen> Uczniowie { get; set; } = new List<Uczen>();
    }
}
