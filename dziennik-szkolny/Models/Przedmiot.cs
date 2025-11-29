
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dziennik_szkolny.Models
{
    public class Przedmiot
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public ICollection<Klasa> Klasy { get; set; } = new List<Klasa>();
    }
}
