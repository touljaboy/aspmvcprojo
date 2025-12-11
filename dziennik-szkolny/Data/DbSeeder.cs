using Microsoft.EntityFrameworkCore;
using Dziennik_szkolny.Data;
using Dziennik_szkolny.Models;

namespace Dziennik_szkolny.Data
{
    public static class DbSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            // Check if data already exists
            if (context.Konto.Any())
            {
                return; // Database has been seeded
            }

            // 1. Create Accounts
            var adminAccount = new Konto { Nazwa = "admin", HasloHash = "admin", Rola = "Admin" };
            var teacherAccount = new Konto { Nazwa = "teacher", HasloHash = "teacher", Rola = "Nauczyciel" };
            var teacher2Account = new Konto { Nazwa = "teacher2", HasloHash = "teacher2", Rola = "Nauczyciel" };
            var studentAccount = new Konto { Nazwa = "student", HasloHash = "student", Rola = "Uczen" };
            var student2Account = new Konto { Nazwa = "student2", HasloHash = "student2", Rola = "Uczen" };
            var parentAccount = new Konto { Nazwa = "parent", HasloHash = "parent", Rola = "Rodzic" };

            context.Konto.AddRange(adminAccount, teacherAccount, teacher2Account, studentAccount, student2Account, parentAccount);
            context.SaveChanges();

            // 2. Create Classes (temporary, will update with supervisors later)
            var klasa1A = new Klasa { NazwaKlasy = "1A" };
            var klasa1B = new Klasa { NazwaKlasy = "1B" };
            var klasa2A = new Klasa { NazwaKlasy = "2A" };

            context.Klasy.AddRange(klasa1A, klasa1B, klasa2A);
            context.SaveChanges();

            // 3. Create Teachers
            var nauczyciel1 = new Nauczyciel
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Email = "jan.kowalski@szkola.pl",
                Telefon = "123456789",
                CzyWychowawca = true,
                KontoId = teacherAccount.Id
            };

            var nauczyciel2 = new Nauczyciel
            {
                Imie = "Anna",
                Nazwisko = "Nowak",
                Email = "anna.nowak@szkola.pl",
                Telefon = "987654321",
                CzyWychowawca = true,
                KontoId = teacher2Account.Id
            };

            context.Nauczyciele.AddRange(nauczyciel1, nauczyciel2);
            context.SaveChanges();

            // 4. Update Classes with Supervisors
            klasa1A.WychowawcaId = nauczyciel1.Id;
            klasa1B.WychowawcaId = nauczyciel2.Id;
            context.SaveChanges();

            // 5. Create Subjects
            var matematyka = new Przedmiot
            {
                Nazwa = "Matematyka",
                TrescKsztalcenia = "Algebra, geometria, funkcje",
                NauczycielId = nauczyciel1.Id
            };

            var polski = new Przedmiot
            {
                Nazwa = "Język Polski",
                TrescKsztalcenia = "Literatura, gramatyka, wypracowania",
                NauczycielId = nauczyciel2.Id
            };

            var historia = new Przedmiot
            {
                Nazwa = "Historia",
                TrescKsztalcenia = "Historia Polski, historia powszechna",
                NauczycielId = nauczyciel2.Id
            };

            var biologia = new Przedmiot
            {
                Nazwa = "Biologia",
                TrescKsztalcenia = "Anatomia, ekologia, genetyka",
                NauczycielId = nauczyciel1.Id
            };

            context.Przedmioty.AddRange(matematyka, polski, historia, biologia);
            context.SaveChanges();

            // 6. Assign Subjects to Classes
            klasa1A.Przedmioty.Add(matematyka);
            klasa1A.Przedmioty.Add(polski);
            klasa1A.Przedmioty.Add(historia);
            klasa1A.Przedmioty.Add(biologia);

            klasa1B.Przedmioty.Add(matematyka);
            klasa1B.Przedmioty.Add(polski);
            klasa1B.Przedmioty.Add(historia);

            klasa2A.Przedmioty.Add(matematyka);
            klasa2A.Przedmioty.Add(polski);
            context.SaveChanges();

            // 7. Create Students
            var uczen1 = new Uczen
            {
                Imie = "Piotr",
                Nazwisko = "Wiśniewski",
                Email = "piotr.wisniewski@student.pl",
                Telefon = "111222333",
                KlasaId = klasa1A.Id,
                KontoId = studentAccount.Id
            };

            var uczen2 = new Uczen
            {
                Imie = "Maria",
                Nazwisko = "Zielińska",
                Email = "maria.zielinska@student.pl",
                Telefon = "444555666",
                KlasaId = klasa1A.Id,
                KontoId = student2Account.Id
            };

            var uczen3 = new Uczen
            {
                Imie = "Tomasz",
                Nazwisko = "Kaczmarek",
                Email = "tomasz.kaczmarek@student.pl",
                Telefon = "777888999",
                KlasaId = klasa1A.Id,
                KontoId = adminAccount.Id // Using admin account as placeholder
            };

            var uczen4 = new Uczen
            {
                Imie = "Katarzyna",
                Nazwisko = "Lewandowska",
                Email = "katarzyna.lewandowska@student.pl",
                Telefon = "123123123",
                KlasaId = klasa1B.Id,
                KontoId = adminAccount.Id // Using admin account as placeholder
            };

            var uczen5 = new Uczen
            {
                Imie = "Michał",
                Nazwisko = "Dąbrowski",
                Email = "michal.dabrowski@student.pl",
                Telefon = "456456456",
                KlasaId = klasa1B.Id,
                KontoId = adminAccount.Id // Using admin account as placeholder
            };

            context.Uczniowie.AddRange(uczen1, uczen2, uczen3, uczen4, uczen5);
            context.SaveChanges();

            // 8. Create Parents
            var rodzic1 = new Rodzic
            {
                Imie = "Barbara",
                Nazwisko = "Wiśniewska",
                Email = "barbara.wisniewska@email.pl",
                Telefon = "555666777",
                UczenId = uczen1.Id,
                KontoId = parentAccount.Id
            };

            var rodzic2 = new Rodzic
            {
                Imie = "Marek",
                Nazwisko = "Zieliński",
                Email = "marek.zielinski@email.pl",
                Telefon = "888999000",
                UczenId = uczen2.Id,
                KontoId = adminAccount.Id // Using admin account as placeholder
            };

            context.Rodzice.AddRange(rodzic1, rodzic2);
            context.SaveChanges();

            // 9. Create Grades
            var oceny = new List<Ocena>
            {
                new Ocena { Wartosc = 5, Opis = "Sprawdzian - równania", Data = new DateTime(2025, 12, 1), UczenId = uczen1.Id, PrzedmiotId = matematyka.Id },
                new Ocena { Wartosc = 4, Opis = "Kartkówka - geometria", Data = new DateTime(2025, 12, 5), UczenId = uczen1.Id, PrzedmiotId = matematyka.Id },
                new Ocena { Wartosc = 5, Opis = "Wypracowanie - wiersz", Data = new DateTime(2025, 11, 28), UczenId = uczen1.Id, PrzedmiotId = polski.Id },
                new Ocena { Wartosc = 3, Opis = "Odpowiedź ustna", Data = new DateTime(2025, 12, 3), UczenId = uczen1.Id, PrzedmiotId = historia.Id },
                new Ocena { Wartosc = 4, Opis = "Test - komórka", Data = new DateTime(2025, 12, 8), UczenId = uczen1.Id, PrzedmiotId = biologia.Id },
                new Ocena { Wartosc = 5, Opis = "Sprawdzian - funkcje", Data = new DateTime(2025, 12, 1), UczenId = uczen2.Id, PrzedmiotId = matematyka.Id },
                new Ocena { Wartosc = 5, Opis = "Praca domowa", Data = new DateTime(2025, 12, 4), UczenId = uczen2.Id, PrzedmiotId = matematyka.Id },
                new Ocena { Wartosc = 4, Opis = "Lektura - omówienie", Data = new DateTime(2025, 11, 30), UczenId = uczen2.Id, PrzedmiotId = polski.Id },
                new Ocena { Wartosc = 5, Opis = "Referat", Data = new DateTime(2025, 12, 2), UczenId = uczen2.Id, PrzedmiotId = historia.Id },
                new Ocena { Wartosc = 3, Opis = "Sprawdzian", Data = new DateTime(2025, 12, 6), UczenId = uczen3.Id, PrzedmiotId = matematyka.Id },
                new Ocena { Wartosc = 4, Opis = "Kartkówka", Data = new DateTime(2025, 12, 7), UczenId = uczen4.Id, PrzedmiotId = matematyka.Id },
                new Ocena { Wartosc = 5, Opis = "Aktywność", Data = new DateTime(2025, 12, 9), UczenId = uczen5.Id, PrzedmiotId = polski.Id }
            };

            context.Oceny.AddRange(oceny);
            context.SaveChanges();

            // 10. Create Announcements
            var ogloszenia = new List<Ogloszenie>
            {
                new Ogloszenie { Tytul = "Ferie zimowe 2026", Tresc = "Ferie zimowe odbędą się w terminie 20.01-02.02.2026. Życzymy udanego wypoczynku!", Data = new DateTime(2025, 12, 1, 10, 0, 0) },
                new Ogloszenie { Tytul = "Zebranie rodziców", Tresc = "Zapraszamy na zebranie rodziców w dniu 15.12.2025 o godzinie 17:00 w salach klasowych.", Data = new DateTime(2025, 12, 5, 14, 30, 0) },
                new Ogloszenie { Tytul = "Konkurs matematyczny", Tresc = "Zachęcamy do udziału w szkolnym konkursie matematycznym. Zapisy do 20.12.2025.", Data = new DateTime(2025, 12, 8, 9, 0, 0) },
                new Ogloszenie { Tytul = "Świąteczne życzenia", Tresc = "Dyrekcja i grono pedagogiczne składają najlepsze życzenia świąteczne i noworoczne!", Data = new DateTime(2025, 12, 10, 12, 0, 0) },
                new Ogloszenie { Tytul = "Zmiana planu lekcji", Tresc = "W czwartek 12.12 nastąpi zmiana planu lekcji z uwagi na szkolenie nauczycieli.", Data = new DateTime(2025, 12, 11, 8, 0, 0) }
            };

            context.Ogloszenie.AddRange(ogloszenia);
            context.SaveChanges();
        }
    }
}
