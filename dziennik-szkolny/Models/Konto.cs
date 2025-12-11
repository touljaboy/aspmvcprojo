using System.ComponentModel.DataAnnotations;

namespace Dziennik_szkolny.Models
{
    public class Konto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nazwa musi mieć od 3 do 50 znaków")]
        public string Nazwa { get; set; } // Login
        
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Hasło musi mieć minimum 3 znaki")]
        public string HasloHash { get; set; } // Zahashowane hasło
        
        [Required(ErrorMessage = "Rola jest wymagana")]
        public string Rola { get; set; } // Np. "Admin", "Nauczyciel", "Uczen", "Rodzic"
    }
}