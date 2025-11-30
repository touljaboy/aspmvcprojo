namespace Dziennik_szkolny.Models
{
    public class Konto
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } // Login
        public string HasloHash { get; set; } // Zahashowane hasło
        public string Rola { get; set; } // Np. "Admin", "Nauczyciel", "Uczen", "Rodzic"
    }
}