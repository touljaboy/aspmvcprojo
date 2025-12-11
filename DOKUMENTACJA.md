
<div style="position: relative; height: 100vh;">
<div style="text-align: center; margin-top: 100px;">

# DZIENNIK SZKOLNY
## System Elektronicznego Dziennika Szkolnego

### Dokumentacja Techniczna i Instrukcja Użytkownika

**Data:** 11 grudnia 2025  
**Technologia:** ASP.NET Core MVC 9.0  
**Baza danych:** SQLite  

</div>

<div style="position: absolute; bottom: 100px; width: 100%; text-align: center;">

---

**Autorzy projektu**  
Ernest Fudali i Grzegorz Czarnopyś

</div>
</div>

<div style="page-break-after: always;"></div>

---

## 1. OPIS PROJEKTU

### 1.1. Cel Projektu

**Dziennik Szkolny** umożliwia zarządzanie ocenami i przedmiotami, ich przeglądanie zarówno przez uczniów jak i przez rodziców.

### 1.2. Przeznaczenie

- **Administrator** - pełna kontrola nad systemem, zarządzanie wszystkimi encjami
- **Nauczyciele** - zarządzanie ocenami, przeglądanie uczniów, dostęp do raportów
- **Uczniowie** - przeglądanie własnych ocen, dostęp do panelu osobistego
- **Rodzice** - monitoring postępów dziecka, przeglądanie ocen i statystyk

---

## 2. ARCHITEKTURA SYSTEMU

### 2.2. Struktura Projektu

```
dziennik-szkolny/
├── Controllers/          # Kontrolery MVC
│   ├── AccountController.cs
│   ├── HomeController.cs
│   ├── StudentController.cs
│   ├── ...
├── Models/               # Modele danych
│   ├── Uczen.cs
│   ├── Nauczyciel.cs
│   ├── ...
├── Views/                # Widoki Razor
│   ├── Account/
│   ├── Home/
│   ├── Uczniowie/
│   ├── ...
├── Data/                 # Kontekst bazy danych
│   ├── ApplicationDbContext.cs
│   └── DbSeeder.cs       # Skrypt zasilajacy baze danych danymi
├── Migrations/           # Migracje EF Core
```

### 2.3. Baza Danych

wzór bazy dostępny w pliku DB_ss.png

## 3. URUCHOMIENIE

Po inicjalizacji bazy danych dostępne są następujące konta testowe:

| Rola | Login | Hasło | Opis |
|------|-------|-------|------|
| Administrator | admin | admin123 | Pełny dostęp do systemu |
| Nauczyciel | kowalski | haslo123 | Jan Kowalski (Wychowawca 1A) |
| Nauczyciel | nowak | haslo123 | Anna Nowak (Matematyka) |
| Uczeń | jkowalski | haslo123 | Jan Kowalski (uczeń) |
| Uczeń | anowak | haslo123 | Anna Nowak (uczennica) |
| Rodzic | kowalscy | haslo123 | Rodzic Jana Kowalskiego |

---

## 4. INSTRUKCJA UŻYTKOWNIKA

### 4.1. Logowanie do Systemu

1. Otwórz przeglądarkę i przejdź do: `http://localhost:5015`
2. Wprowadź nazwę użytkownika i hasło
3. Kliknij **"Zaloguj"**

Po poprawnym zalogowaniu zostaniesz przekierowany do panelu odpowiedniego dla Twojej roli.

### 4.2. Panel Administratora

Administrator ma pełny dostęp do funkcji systemu związanych z API bazy danych.

#### 4.2.1. Strona główna

Po zalogowaniu administrator widzi menu nawigacyjne z następującymi opcjami:

- **Dziennik szkolny** - powrót na stronę główną
- **Uczniowie** - zarządzanie uczniami
- **Nauczyciele** - zarządzanie nauczycielami
- **Rodzice** - zarządzanie rodzicami
- **Klasy** - zarządzanie klasami
- **Przedmioty** - zarządzanie przedmiotami
- **Oceny** - przeglądanie i zarządzanie ocenami
- **Konta** - zarządzanie kontami użytkowników
- **Ogłoszenia** - tworzenie ogłoszeń

#### 4.2.2. Zarządzanie Uczniami

**Dodawanie nowego ucznia:**

1. Przejdź do **Uczniowie** → **Utwórz nowy**
2. Wypełnij formularz:
   - Imię (wymagane)
   - Nazwisko (wymagane)
   - Email (opcjonalnie, z walidacją)
   - Telefon (opcjonalnie, z walidacją)
   - Klasa (wybierz z listy)
   - Konto (wybierz z listy)
3. Kliknij **Utwórz**

**Edycja ucznia:**

1. Przejdź do **Uczniowie**
2. Kliknij **Edytuj** przy wybranym uczniu
3. Zmień dane i kliknij **Zapisz**

**Usuwanie ucznia:**

1. Przejdź do **Uczniowie**
2. Kliknij **Usuń** przy wybranym uczniu
3. Potwierdź operację klikając **Usuń**


#### 4.2.3. Zarządzanie Nauczycielami

**Dodawanie nauczyciela:**

1. Przejdź do **Nauczyciele** → **Utwórz nowy**
2. Wypełnij formularz:
   - Imię (wymagane)
   - Nazwisko (wymagane)
   - Email (opcjonalnie)
   - Telefon (opcjonalnie)
   - Czy wychowawca? (checkbox)
   - Konto (wybierz z listy)
   - Przełożony (opcjonalnie)
3. Kliknij **Utwórz**

**Lista nauczycieli** wyświetla:
- Imię i nazwisko
- Email
- Telefon
- Status wychowawcy
- Przypisane konto
- Przełożony (jeśli jest)
- Akcje (Edytuj, Szczegóły, Usuń)

#### 4.2.4. Zarządzanie Klasami

**Dodawanie klasy:**

1. Przejdź do **Klasy** → **Utwórz nowy**
2. Wprowadź nazwę klasy (np. "1A", "2B")
3. Wybierz wychowawcę z listy nauczycieli
4. Kliknij **Utwórz**

**Przypisywanie przedmiotów do klasy:**

1. Przejdź do **Przedmioty**
2. Edytuj przedmiot
3. Wybierz klasy, do których ma być przypisany przedmiot

#### 4.2.5. Zarządzanie Przedmiotami

**Dodawanie przedmiotu:**

1. Przejdź do **Przedmioty** → **Utwórz nowy**
2. Wypełnij formularz:
   - Nazwa przedmiotu (np. "Matematyka")
   - Nauczyciel (wybierz z listy)
   - Klasy (zaznacz odpowiednie klasy)
3. Kliknij **Utwórz**

#### 4.2.6. Zarządzanie Ocenami

**Dodawanie oceny:**

1. Przejdź do **Oceny** → **Utwórz nowy**
2. Wypełnij formularz:
   - Wartość (1-6)
   - Opis (np. "Kartkówka z trygonometrii")
   - Data
   - Uczeń (wybierz z listy)
   - Przedmiot (wybierz z listy)
3. Kliknij **Utwórz**

**Lista ocen** wyświetla oceny z kolorowaniem:
- **Zielony** (5-6) - oceny bardzo dobre i celujące
- **Niebieski** (4-4.99) - oceny dobre
- **Żółty** (3-3.99) - oceny dostateczne
- **Czerwony** (<3) - oceny niedostateczne

#### 4.2.7. Zarządzanie Kontami

**Dodawanie konta:**

1. Przejdź do **Konta** → **Utwórz nowy**
2. Wypełnij formularz:
   - Nazwa użytkownika (login)
   - Hasło
   - Rola (Admin/Nauczyciel/Uczen/Rodzic)
3. Kliknij **Utwórz**

**Zmiana hasła:**

1. Edytuj konto
2. Wprowadź nowe hasło
3. Zapisz zmiany


#### 4.2.8. Ogłoszenia

**Tworzenie ogłoszenia:**

1. Przejdź do **Ogłoszenia** → **Utwórz nowy**
2. Wypełnij formularz:
   - Tytuł
   - Treść
   - Data (automatycznie ustawiana)
3. Kliknij **Utwórz**

Ogłoszenia są widoczne na dashboardach wszystkich użytkowników.

### 4.3. Panel Nauczyciela

Po zalogowaniu nauczyciel ma dostęp do:

#### 4.3.1. Dashboard Nauczyciela

Dashboard wyświetla:
- **Przedmioty prowadzone** - lista przedmiotów nauczyciela
- **Klasy, którym uczy** - wszystkie klasy, w których nauczyciel prowadzi zajęcia
- **Klasy wychowawcze** - jeśli nauczyciel jest wychowawcą
- **Uczniowie** - w klasach wychowawczych
- **Ostatnie ogłoszenia** - 5 najnowszych ogłoszeń

#### 4.3.2. Menu Nauczyciela

- **Dashboard** - strona główna nauczyciela
- **Moi Uczniowie** - lista wszystkich uczniów w klasach nauczyciela
- **Oceny** - przeglądanie i zarządzanie ocenami
- **Raporty** - zaawansowane raporty i statystyki
- **Mój Profil** - edycja danych osobowych

#### 4.3.3. Przeglądanie Ocen Uczniów

1. Przejdź do **Moi Uczniowie**
2. Kliknij **Zobacz oceny** przy wybranym uczniu
3. Zobaczysz wszystkie oceny ucznia z kolorowaniem
4. Możliwość filtrowania po przedmiocie

**Funkcje:**
- Wyświetlanie ocen z kolorowaniem
- Średnia ocen dla każdego przedmiotu
- Średnia ogólna ucznia

#### 4.3.4. Raporty

Nauczyciel ma dostęp do trzech rodzajów raportów:

**1. Raport ogólny:**
- Wszystkie oceny z wybranego okresu
- Możliwość filtrowania po:
  - Dacie
  - Uczniu
  - Klasie
  - Przedmiocie

**2. Raport uczniów:**
- Statystyki dla każdego ucznia:
  - Liczba ocen
  - Średnia ocen (kolorowana)
  - Klasa

**3. Raport przedmiotów:**
- Statystyki dla każdego przedmiotu:
  - Liczba klas
  - Liczba ocen
  - Średnia ocen (kolorowana)

#### 4.3.5. Mój Profil (Nauczyciel)

1. Kliknij **Mój Profil** w menu
2. Zobacz swoje dane:
   - Imię i nazwisko
   - Email
   - Telefon
   - Czy jesteś wychowawcą
   - Przypisane konto
3. Kliknij **Edytuj Profil** aby zmienić:
   - Email
   - Telefon

**UWAGA:** Nauczyciel może edytować tylko email i telefon. Inne dane może zmienić tylko administrator.

### 4.4. Panel Ucznia

#### 4.4.1. Dashboard Ucznia

Dashboard wyświetla:
- **Informacje osobiste**:
  - Imię i nazwisko
  - Klasa
  - Email
  - Telefon
- **Ostatnie oceny** - 5 najnowszych ocen z kolorowaniem
- **Ostatnie ogłoszenia** - 5 najnowszych ogłoszeń

#### 4.4.2. Menu Ucznia

- **Dashboard** - strona główna
- **Moje Oceny** - wszystkie oceny
- **Mój Profil** - edycja danych osobowych

#### 4.4.3. Moje Oceny

Widok **Moje Oceny** zawiera:

1. **Statystyki przedmiotowe:**
   - Nazwa przedmiotu
   - Liczba ocen
   - Średnia (kolorowana)

2. **Lista wszystkich ocen:**
   - Data
   - Przedmiot
   - Ocena (kolorowana)
   - Opis

3. **Średnia ogólna** - na dole strony 

**Kolorowanie ocen:**
- Zielony (≥5.0)
- Niebieski (≥4.0)
- Żółty (≥3.0)
- Czerwony (<3.0)

#### 4.4.4. Mój Profil (Uczeń)

1. Kliknij **Mój Profil**
2. Zobacz swoje dane
3. Kliknij **Edytuj Profil** aby zmienić email i telefon

### 4.5. Panel Rodzica

#### 4.5.1. Dashboard Rodzica

Dashboard wyświetla:
- **Informacje o dziecku**:
  - Imię i nazwisko
  - Klasa
  - Wychowawca
- **Ostatnie oceny dziecka** - 5 najnowszych ocen (kolorowane)
- **Ostatnie ogłoszenia**

#### 4.5.2. Menu Rodzica

- **Dashboard** - strona główna
- **Oceny Dziecka** - wszystkie oceny dziecka
- **Mój Profil** - edycja danych osobowych

#### 4.5.3. Oceny Dziecka

Widok zawiera:

1. **Statystyki przedmiotowe** (jak u ucznia):
   - Przedmiot
   - Liczba ocen
   - Średnia (kolorowana)

2. **Lista wszystkich ocen dziecka:**
   - Data
   - Przedmiot
   - Ocena (kolorowana)
   - Opis

3. **Średnia ogólna dziecka** (kolorowana)

**Funkcje dla rodzica:**
- Pełny monitoring postępów dziecka
- Szczegółowe statystyki przedmiotowe
- Wizualizacja ocen przez kolorowanie

#### 4.5.4. Mój Profil (Rodzic)

Analogicznie jak u innych ról - możliwość edycji email i telefonu.

### 4.6. Wylogowanie

Aby wylogować się z systemu:

1. Kliknij **Wyloguj** w prawym górnym rogu menu
2. Zostaniesz przekierowany na stronę logowania

---

## 5. ZREALIZOWANE FUNKCJONALNOŚCI

### 5.1. Podstawowe Funkcjonalności (Punktowane)

#### modyfikacja listy uczniów należących do poszczególnych klas - **2 pkt**

**Realizacja:**
- Pełny CRUD (Create, Read, Update, Delete) dla uczniów
- Przypisywanie uczniów do klas
- Walidacja danych (imię, nazwisko wymagane)
- Powiązanie z kontem użytkownika

**Kontroler:** `UczniowieController.cs`  
**Widoki:** `Views/Uczniowie/`

#### tworzenie listy przedmiotów oraz przyporządkowywanie ich do różnych klas - **2 pkt**

**Realizacja:**
- CRUD dla przedmiotów
- Relacja wiele-do-wielu między przedmiotami a klasami
- Przypisywanie nauczycieli do przedmiotów
- Lista klas dla każdego przedmiotu

**Kontroler:** `PrzedmiotyController.cs`  
**Widoki:** `Views/Przedmioty/`

#### Dodawanie nauczycieli do przedmiotów i wychowawców do klas - **2 pkt**

**Realizacja:**
- CRUD dla nauczycieli
- Przypisywanie nauczyciela jako wychowawcy klasy (pole `WychowawcaId` w klasie)
- Przypisywanie nauczyciela do przedmiotu (pole `NauczycielId`)

**Kontrolery:** `NauczycieleController.cs`, `KlasyController.cs`  
**Widoki:** `Views/Nauczyciele/`, `Views/Klasy/`

#### możliwość edycji danych osobowych uczniów, nauczycieli oraz rodziców w ramach ich profili użytkowników - **4 pkt**

**Realizacja:**
- Osobne profile dla uczniów, nauczycieli i rodziców
- Funkcje `MyProfile` i `EditProfile` w kontrolerach:
  - `StudentController.cs`
  - `TeacherController.cs`
  - `ParentController.cs`
- Bezpieczna edycja (tylko email i telefon)

#### konta nauczycieli, uczniów oraz rodziców - **2 pkt**

**Realizacja:**
- System autentykacji oparty na Cookie Authentication
- 4 role: Admin, Nauczyciel, Uczen, Rodzic
- Model `Konto` z polami: Id, Nazwa, HasloHash, Rola

**Kontroler:** `AccountController.cs`  
**Serwis:** `AuthService.cs`

**Funkcje:**
- Logowanie (`Login`)
- Wylogowanie (`Logout`)
- Odmowa dostępu (`AccessDenied`)
- Sprawdzanie uprawnień w kontrolerach

#### przeglądanie zestawień ocen z zadanego okresu dla wybranych uczniów oraz przedmiotów - **2 pkt**

**Realizacja:**
- Moduł raportów dostępny dla nauczycieli i administratorów
- 3 typy raportów:
  1. **Raport ogólny** - wszystkie oceny z filtrowaniem
  2. **Raport uczniów** - statystyki per uczeń
  3. **Raport przedmiotów** - statystyki per przedmiot

**Kontroler:** `RaportyController.cs`  
**Widoki:** `Views/Raporty/`

**Funkcje filtrowania:**
- Data od/do
- Wybór ucznia
- Wybór klasy
- Wybór przedmiotu

#### lista ogłoszeń na głównej stronie serwisu - **2 pkt**

**Realizacja:**
- Lista ogłoszeń widoczna na dashboardach wszystkich użytkowników
- 5 najnowszych ogłoszeń wyświetlanych na stronie głównej każdej roli
- CRUD dla ogłoszeń (dostępny dla administratora)
- Model `Ogloszenie` z polami: Tytuł, Treść, Data
- Sortowanie ogłoszeń po dacie (najnowsze na górze)

**Kontroler:** `OgloszeniaController.cs`  
**Widoki:** `Views/Ogloszenia/`

**Wyświetlanie na dashboardach:**
- **Dashboard Ucznia** - sekcja "Ogłoszenia" z 5 najnowszymi
- **Dashboard Nauczyciela** - sekcja "Ostatnie ogłoszenia" 
- **Dashboard Rodzica** - sekcja "Ogłoszenia"
- **Strona główna (Home)** - lista wszystkich ogłoszeń dla niezalogowanych/administratorów

**Funkcjonalności:**
- Tworzenie ogłoszeń przez administratora
- Wyświetlanie tytułu, treści i daty
- Edycja i usuwanie ogłoszeń (admin)

<div style="margin-top: 300px;">

### 5.3. Podsumowanie Punktacji

| Funkcjonalność | Punkty |
|----------------|--------|
| Modyfikacja listy uczniów | 2 |
| Lista przedmiotów i przypisanie do klas | 2 |
| Nauczyciele do przedmiotów i wychowawcy | 2 |
| Edycja danych osobowych w profilach | 4 |
| Konta dla wszystkich ról | 2 |
| Przeglądanie zestawień ocen | 2 |
| Lista ogłoszeń na głównej stronie | 2 |
| **SUMA** | **16** |

---