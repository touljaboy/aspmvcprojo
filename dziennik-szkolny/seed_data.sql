-- Seed data for Dziennik Szkolny
-- This script creates demo accounts and sample data

-- 1. Create Accounts (Konta)
INSERT INTO Konto (Nazwa, HasloHash, Rola) VALUES ('admin', 'admin', 'Admin');
INSERT INTO Konto (Nazwa, HasloHash, Rola) VALUES ('teacher', 'teacher', 'Nauczyciel');
INSERT INTO Konto (Nazwa, HasloHash, Rola) VALUES ('teacher2', 'teacher2', 'Nauczyciel');
INSERT INTO Konto (Nazwa, HasloHash, Rola) VALUES ('student', 'student', 'Uczen');
INSERT INTO Konto (Nazwa, HasloHash, Rola) VALUES ('student2', 'student2', 'Uczen');
INSERT INTO Konto (Nazwa, HasloHash, Rola) VALUES ('parent', 'parent', 'Rodzic');

-- 2. Create Classes (Klasy)
INSERT INTO Klasy (NazwaKlasy, WychowawcaId) VALUES ('1A', NULL);
INSERT INTO Klasy (NazwaKlasy, WychowawcaId) VALUES ('1B', NULL);
INSERT INTO Klasy (NazwaKlasy, WychowawcaId) VALUES ('2A', NULL);

-- 3. Create Teachers (Nauczyciele)
INSERT INTO Nauczyciele (Imie, Nazwisko, Email, Telefon, CzyWychowawca, KontoId, PrzelozonyId) 
VALUES ('Jan', 'Kowalski', 'jan.kowalski@szkola.pl', '123456789', 1, 2, NULL);

INSERT INTO Nauczyciele (Imie, Nazwisko, Email, Telefon, CzyWychowawca, KontoId, PrzelozonyId) 
VALUES ('Anna', 'Nowak', 'anna.nowak@szkola.pl', '987654321', 0, 3, NULL);

-- 4. Update Classes with Supervisors
UPDATE Klasy SET WychowawcaId = 1 WHERE NazwaKlasy = '1A';
UPDATE Klasy SET WychowawcaId = 2 WHERE NazwaKlasy = '1B';

-- 5. Create Subjects (Przedmioty)
INSERT INTO Przedmioty (Nazwa, TrescKsztalcenia, NauczycielId) 
VALUES ('Matematyka', 'Algebra, geometria, funkcje', 1);

INSERT INTO Przedmioty (Nazwa, TrescKsztalcenia, NauczycielId) 
VALUES ('Język Polski', 'Literatura, gramatyka, wypracowania', 2);

INSERT INTO Przedmioty (Nazwa, TrescKsztalcenia, NauczycielId) 
VALUES ('Historia', 'Historia Polski, historia powszechna', 2);

INSERT INTO Przedmioty (Nazwa, TrescKsztalcenia, NauczycielId) 
VALUES ('Biologia', 'Anatomia, ekologia, genetyka', 1);

-- 6. Assign Subjects to Classes (KlasaPrzedmiot)
INSERT INTO KlasaPrzedmiot (KlasyId, PrzedmiotyId) VALUES (1, 1); -- 1A - Matematyka
INSERT INTO KlasaPrzedmiot (KlasyId, PrzedmiotyId) VALUES (1, 2); -- 1A - Język Polski
INSERT INTO KlasaPrzedmiot (KlasyId, PrzedmiotyId) VALUES (1, 3); -- 1A - Historia
INSERT INTO KlasaPrzedmiot (KlasyId, PrzedmiotyId) VALUES (1, 4); -- 1A - Biologia

INSERT INTO KlasaPrzedmiot (KlasyId, PrzedmiotyId) VALUES (2, 1); -- 1B - Matematyka
INSERT INTO KlasaPrzedmiot (KlasyId, PrzedmiotyId) VALUES (2, 2); -- 1B - Język Polski
INSERT INTO KlasaPrzedmiot (KlasyId, PrzedmiotyId) VALUES (2, 3); -- 1B - Historia

INSERT INTO KlasaPrzedmiot (KlasyId, PrzedmiotyId) VALUES (3, 1); -- 2A - Matematyka
INSERT INTO KlasaPrzedmiot (KlasyId, PrzedmiotyId) VALUES (3, 2); -- 2A - Język Polski

-- 7. Create Students (Uczniowie)
INSERT INTO Uczniowie (Imie, Nazwisko, Email, Telefon, KlasaId, KontoId) 
VALUES ('Piotr', 'Wiśniewski', 'piotr.wisniewski@student.pl', '111222333', 1, 4);

INSERT INTO Uczniowie (Imie, Nazwisko, Email, Telefon, KlasaId, KontoId) 
VALUES ('Maria', 'Zielińska', 'maria.zielinska@student.pl', '444555666', 1, 5);

INSERT INTO Uczniowie (Imie, Nazwisko, Email, Telefon, KlasaId, KontoId) 
VALUES ('Tomasz', 'Kaczmarek', 'tomasz.kaczmarek@student.pl', '777888999', 1, NULL);

INSERT INTO Uczniowie (Imie, Nazwisko, Email, Telefon, KlasaId, KontoId) 
VALUES ('Katarzyna', 'Lewandowska', 'katarzyna.lewandowska@student.pl', '123123123', 2, NULL);

INSERT INTO Uczniowie (Imie, Nazwisko, Email, Telefon, KlasaId, KontoId) 
VALUES ('Michał', 'Dąbrowski', 'michal.dabrowski@student.pl', '456456456', 2, NULL);

-- 8. Create Parents (Rodzice)
INSERT INTO Rodzice (Imie, Nazwisko, Email, Telefon, UczenId, KontoId) 
VALUES ('Barbara', 'Wiśniewska', 'barbara.wisniewska@email.pl', '555666777', 1, 6);

INSERT INTO Rodzice (Imie, Nazwisko, Email, Telefon, UczenId, KontoId) 
VALUES ('Marek', 'Zieliński', 'marek.zielinski@email.pl', '888999000', 2, NULL);

-- 9. Create Grades (Oceny)
INSERT INTO Oceny (Wartosc, Opis, Data, UczenId, PrzedmiotId) 
VALUES (5, 'Sprawdzian - równania', '2025-12-01', 1, 1);

INSERT INTO Oceny (Wartosc, Opis, Data, UczenId, PrzedmiotId) 
VALUES (4, 'Kartkówka - geometria', '2025-12-05', 1, 1);

INSERT INTO Oceny (Wartosc, Opis, Data, UczenId, PrzedmiotId) 
VALUES (5, 'Wypracowanie - wiersz', '2025-11-28', 1, 2);

INSERT INTO Oceny (Wartosc, Opis, Data, UczenId, PrzedmiotId) 
VALUES (3, 'Odpowiedź ustna', '2025-12-03', 1, 3);

INSERT INTO Oceny (Wartosc, Opis, Data, UczenId, PrzedmiotId) 
VALUES (4, 'Test - komórka', '2025-12-08', 1, 4);

INSERT INTO Oceny (Wartosc, Opis, Data, UczenId, PrzedmiotId) 
VALUES (5, 'Sprawdzian - funkcje', '2025-12-01', 2, 1);

INSERT INTO Oceny (Wartosc, Opis, Data, UczenId, PrzedmiotId) 
VALUES (5, 'Praca domowa', '2025-12-04', 2, 1);

INSERT INTO Oceny (Wartosc, Opis, Data, UczenId, PrzedmiotId) 
VALUES (4, 'Lektura - omówienie', '2025-11-30', 2, 2);

INSERT INTO Oceny (Wartosc, Opis, Data, UczenId, PrzedmiotId) 
VALUES (5, 'Referat', '2025-12-02', 2, 3);

INSERT INTO Oceny (Wartosc, Opis, Data, UczenId, PrzedmiotId) 
VALUES (3, 'Sprawdzian', '2025-12-06', 3, 1);

INSERT INTO Oceny (Wartosc, Opis, Data, UczenId, PrzedmiotId) 
VALUES (4, 'Kartkówka', '2025-12-07', 4, 1);

INSERT INTO Oceny (Wartosc, Opis, Data, UczenId, PrzedmiotId) 
VALUES (5, 'Aktywność', '2025-12-09', 5, 2);

-- 10. Create Announcements (Ogloszenia)
INSERT INTO Ogloszenie (Tytul, Tresc, Data) 
VALUES ('Ferie zimowe 2026', 'Ferie zimowe odbędą się w terminie 20.01-02.02.2026. Życzymy udanego wypoczynku!', '2025-12-01 10:00:00');

INSERT INTO Ogloszenie (Tytul, Tresc, Data) 
VALUES ('Zebranie rodziców', 'Zapraszamy na zebranie rodziców w dniu 15.12.2025 o godzinie 17:00 w salach klasowych.', '2025-12-05 14:30:00');

INSERT INTO Ogloszenie (Tytul, Tresc, Data) 
VALUES ('Konkurs matematyczny', 'Zachęcamy do udziału w szkolnym konkursie matematycznym. Zapisy do 20.12.2025.', '2025-12-08 09:00:00');

INSERT INTO Ogloszenie (Tytul, Tresc, Data) 
VALUES ('Świąteczne życzenia', 'Dyrekcja i grono pedagogiczne składają najlepsze życzenia świąteczne i noworoczne!', '2025-12-10 12:00:00');

INSERT INTO Ogloszenie (Tytul, Tresc, Data) 
VALUES ('Zmiana planu lekcji', 'W czwartek 12.12 nastąpi zmiana planu lekcji z uwagi na szkolenie nauczycieli.', '2025-12-11 08:00:00');
