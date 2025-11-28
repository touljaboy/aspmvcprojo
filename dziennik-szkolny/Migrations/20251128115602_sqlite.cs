using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dziennik_szkolny.Migrations
{
    /// <inheritdoc />
    public partial class sqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klasy",
                columns: table => new
                {
                    Id_klasy = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NazwaKlasy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klasy", x => x.Id_klasy);
                });

            migrationBuilder.CreateTable(
                name: "Nauczyciele",
                columns: table => new
                {
                    Id_wychowawcy = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", nullable: false),
                    Nazwisko = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nauczyciele", x => x.Id_wychowawcy);
                });

            migrationBuilder.CreateTable(
                name: "Rodzice",
                columns: table => new
                {
                    Id_rodzica = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", nullable: false),
                    Nazwisko = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rodzice", x => x.Id_rodzica);
                });

            migrationBuilder.CreateTable(
                name: "Przedmioty",
                columns: table => new
                {
                    Id_przedmiotu = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NazwaPrzedmiotu = table.Column<string>(type: "TEXT", nullable: false),
                    Id_nauczyciela = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przedmioty", x => x.Id_przedmiotu);
                    table.ForeignKey(
                        name: "FK_Przedmioty_Nauczyciele_Id_nauczyciela",
                        column: x => x.Id_nauczyciela,
                        principalTable: "Nauczyciele",
                        principalColumn: "Id_wychowawcy",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Uczniowie",
                columns: table => new
                {
                    Id_Ucznia = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", nullable: false),
                    Nazwisko = table.Column<string>(type: "TEXT", nullable: false),
                    Id_rodzica = table.Column<int>(type: "INTEGER", nullable: false),
                    Id_klasy = table.Column<int>(type: "INTEGER", nullable: false),
                    Id_wychowawcy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uczniowie", x => x.Id_Ucznia);
                    table.ForeignKey(
                        name: "FK_Uczniowie_Klasy_Id_klasy",
                        column: x => x.Id_klasy,
                        principalTable: "Klasy",
                        principalColumn: "Id_klasy",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uczniowie_Nauczyciele_Id_wychowawcy",
                        column: x => x.Id_wychowawcy,
                        principalTable: "Nauczyciele",
                        principalColumn: "Id_wychowawcy",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uczniowie_Rodzice_Id_rodzica",
                        column: x => x.Id_rodzica,
                        principalTable: "Rodzice",
                        principalColumn: "Id_rodzica",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KlasaPrzedmiot",
                columns: table => new
                {
                    KlasyId_klasy = table.Column<int>(type: "INTEGER", nullable: false),
                    PrzedmiotyId_przedmiotu = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlasaPrzedmiot", x => new { x.KlasyId_klasy, x.PrzedmiotyId_przedmiotu });
                    table.ForeignKey(
                        name: "FK_KlasaPrzedmiot_Klasy_KlasyId_klasy",
                        column: x => x.KlasyId_klasy,
                        principalTable: "Klasy",
                        principalColumn: "Id_klasy",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KlasaPrzedmiot_Przedmioty_PrzedmiotyId_przedmiotu",
                        column: x => x.PrzedmiotyId_przedmiotu,
                        principalTable: "Przedmioty",
                        principalColumn: "Id_przedmiotu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Oceny",
                columns: table => new
                {
                    Id_oceny = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Wartosc = table.Column<int>(type: "INTEGER", nullable: false),
                    DataWpisu = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Id_Ucznia = table.Column<int>(type: "INTEGER", nullable: false),
                    Id_przedmiotu = table.Column<int>(type: "INTEGER", nullable: false),
                    Id_nauczyciela = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oceny", x => x.Id_oceny);
                    table.ForeignKey(
                        name: "FK_Oceny_Nauczyciele_Id_nauczyciela",
                        column: x => x.Id_nauczyciela,
                        principalTable: "Nauczyciele",
                        principalColumn: "Id_wychowawcy",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Oceny_Przedmioty_Id_przedmiotu",
                        column: x => x.Id_przedmiotu,
                        principalTable: "Przedmioty",
                        principalColumn: "Id_przedmiotu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Oceny_Uczniowie_Id_Ucznia",
                        column: x => x.Id_Ucznia,
                        principalTable: "Uczniowie",
                        principalColumn: "Id_Ucznia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KlasaPrzedmiot_PrzedmiotyId_przedmiotu",
                table: "KlasaPrzedmiot",
                column: "PrzedmiotyId_przedmiotu");

            migrationBuilder.CreateIndex(
                name: "IX_Oceny_Id_nauczyciela",
                table: "Oceny",
                column: "Id_nauczyciela");

            migrationBuilder.CreateIndex(
                name: "IX_Oceny_Id_przedmiotu",
                table: "Oceny",
                column: "Id_przedmiotu");

            migrationBuilder.CreateIndex(
                name: "IX_Oceny_Id_Ucznia",
                table: "Oceny",
                column: "Id_Ucznia");

            migrationBuilder.CreateIndex(
                name: "IX_Przedmioty_Id_nauczyciela",
                table: "Przedmioty",
                column: "Id_nauczyciela");

            migrationBuilder.CreateIndex(
                name: "IX_Uczniowie_Id_klasy",
                table: "Uczniowie",
                column: "Id_klasy");

            migrationBuilder.CreateIndex(
                name: "IX_Uczniowie_Id_rodzica",
                table: "Uczniowie",
                column: "Id_rodzica");

            migrationBuilder.CreateIndex(
                name: "IX_Uczniowie_Id_wychowawcy",
                table: "Uczniowie",
                column: "Id_wychowawcy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KlasaPrzedmiot");

            migrationBuilder.DropTable(
                name: "Oceny");

            migrationBuilder.DropTable(
                name: "Przedmioty");

            migrationBuilder.DropTable(
                name: "Uczniowie");

            migrationBuilder.DropTable(
                name: "Klasy");

            migrationBuilder.DropTable(
                name: "Nauczyciele");

            migrationBuilder.DropTable(
                name: "Rodzice");
        }
    }
}
