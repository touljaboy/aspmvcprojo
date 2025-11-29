# Requirements (in polish):
6. Wirtualny dziennik szkolny
Tematem projektu jest elektroniczny dziennik szkolny. Aplikacja umożliwia zarządzanie
ocenami i przedmiotami, ich przeglądanie zarówno przez uczniów jak i przez rodziców.
Funkcjonalności:
- modyfikacja listy uczniów należących do poszczególnych klas – 2 pkt.,
-  tworzenie listy przedmiotów oraz przyporządkowywanie ich do różnych klas – 2 pkt,
-   dodawanie nauczycieli do przedmiotów oraz wychowawców dla klas – 2 pkt,
-    możliwość edycji danych osobowych uczniów, nauczycieli oraz rodziców w ramach ich
profili użytkowników – 4 pkt,
- konta nauczycieli, uczniów oraz rodziców – 2 pkt,
- przeglądanie zestawień ocen z zadanego okresu dla wybranych uczniów oraz przedmiotów - 2 pkt,
- lista ogłoszeń na głównej stronie serwisu – 2 pkt,
- umieszczanie treści kształcenia przyporządkowanych do przedmiotów – 2 pkt,
- dodatkowe wersje językowe interfejsu użytkownika – 2 pkt. za pierwszy dodatkowy język i
1 pkt. za drugi.


# komenda do uruchamiania
```
dotnet watch run
```


# Instalowanie paczek

zamiast tych komend, powinno zadzialac zwykle:
```
dotnet restore
```


po kolej:
```
dotnet add package Microsoft.EntityFrameworkCore
```
```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```
```
dotnet add package Microsoft.EntityFrameworkCore.Tools
```
```
dotnet add package Microsoft.EntityFrameworkCore.Design
```
```
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

zamknij o tworz terminal po tych wszystkich komendach 
## migracje

Zainstaluj SQL server express z instalki dostępnej [tutaj](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
i tam wybierz custom.
i zaznacz "localdb"
i to sie pobierze i zainstaluj.

```
dotnet tool update -g dotnet-aspnet-codegenerator
```
```
dotnet tool install --global dotnet-ef
```
```
dotnet ef migrations add InitialCreate
```
```
dotnet ef database update
```

Zainstaluj dotnet 8 ze stronki microsoftu. Potem odpal te komendy:
```
dotnet tool install -g dotnet-aspnet-codegenerator 
```

**Generowanie controllers i views**
| Model | Kontroler | Polecenie do uruchomienia |
| :--- | :--- | :--- |
| **Uczen** | `UczniowieController` | `dotnet aspnet-codegenerator controller -name UczniowieController -m Uczen -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout` |
| **Rodzic** | `RodziceController` | `dotnet aspnet-codegenerator controller -name RodziceController -m Rodzic -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout` |
| **Nauczyciel** | `NauczycieleController` | `dotnet aspnet-codegenerator controller -name NauczycieleController -m Nauczyciel -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout` |
| **Klasa** | `KlasyController` | `dotnet aspnet-codegenerator controller -name KlasyController -m Klasa -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout` |
| **Przedmiot** | `PrzedmiotyController` | `dotnet aspnet-codegenerator controller -name PrzedmiotyController -m Przedmiot -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout` |
| **Ocena** | `OcenyController` | `dotnet aspnet-codegenerator controller -name OcenyController -m Ocena -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout` |
