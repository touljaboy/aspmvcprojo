using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dziennik_szkolny.Migrations
{
    /// <inheritdoc />
    public partial class modelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KlasaPrzedmiot_Klasy_KlasyId_klasy",
                table: "KlasaPrzedmiot");

            migrationBuilder.DropForeignKey(
                name: "FK_KlasaPrzedmiot_Przedmioty_PrzedmiotyId_przedmiotu",
                table: "KlasaPrzedmiot");

            migrationBuilder.DropForeignKey(
                name: "FK_Oceny_Nauczyciele_Id_nauczyciela",
                table: "Oceny");

            migrationBuilder.DropForeignKey(
                name: "FK_Oceny_Przedmioty_Id_przedmiotu",
                table: "Oceny");

            migrationBuilder.DropForeignKey(
                name: "FK_Oceny_Uczniowie_Id_Ucznia",
                table: "Oceny");

            migrationBuilder.DropForeignKey(
                name: "FK_Przedmioty_Nauczyciele_Id_nauczyciela",
                table: "Przedmioty");

            migrationBuilder.DropForeignKey(
                name: "FK_Uczniowie_Klasy_Id_klasy",
                table: "Uczniowie");

            migrationBuilder.DropForeignKey(
                name: "FK_Uczniowie_Nauczyciele_Id_wychowawcy",
                table: "Uczniowie");

            migrationBuilder.DropForeignKey(
                name: "FK_Uczniowie_Rodzice_Id_rodzica",
                table: "Uczniowie");

            migrationBuilder.DropIndex(
                name: "IX_Uczniowie_Id_klasy",
                table: "Uczniowie");

            migrationBuilder.DropIndex(
                name: "IX_Przedmioty_Id_nauczyciela",
                table: "Przedmioty");

            migrationBuilder.DropIndex(
                name: "IX_Oceny_Id_Ucznia",
                table: "Oceny");

            migrationBuilder.DropColumn(
                name: "Id_klasy",
                table: "Uczniowie");

            migrationBuilder.DropColumn(
                name: "Id_nauczyciela",
                table: "Przedmioty");

            migrationBuilder.DropColumn(
                name: "Id_Ucznia",
                table: "Oceny");

            migrationBuilder.RenameColumn(
                name: "Id_wychowawcy",
                table: "Uczniowie",
                newName: "KontoId");

            migrationBuilder.RenameColumn(
                name: "Id_rodzica",
                table: "Uczniowie",
                newName: "KlasaId");

            migrationBuilder.RenameColumn(
                name: "Id_Ucznia",
                table: "Uczniowie",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Uczniowie_Id_wychowawcy",
                table: "Uczniowie",
                newName: "IX_Uczniowie_KontoId");

            migrationBuilder.RenameIndex(
                name: "IX_Uczniowie_Id_rodzica",
                table: "Uczniowie",
                newName: "IX_Uczniowie_KlasaId");

            migrationBuilder.RenameColumn(
                name: "Id_rodzica",
                table: "Rodzice",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "NazwaPrzedmiotu",
                table: "Przedmioty",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "Id_przedmiotu",
                table: "Przedmioty",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Id_przedmiotu",
                table: "Oceny",
                newName: "UczenId");

            migrationBuilder.RenameColumn(
                name: "Id_nauczyciela",
                table: "Oceny",
                newName: "PrzedmiotId");

            migrationBuilder.RenameColumn(
                name: "DataWpisu",
                table: "Oceny",
                newName: "Opis");

            migrationBuilder.RenameColumn(
                name: "Id_oceny",
                table: "Oceny",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Oceny_Id_przedmiotu",
                table: "Oceny",
                newName: "IX_Oceny_UczenId");

            migrationBuilder.RenameIndex(
                name: "IX_Oceny_Id_nauczyciela",
                table: "Oceny",
                newName: "IX_Oceny_PrzedmiotId");

            migrationBuilder.RenameColumn(
                name: "Id_wychowawcy",
                table: "Nauczyciele",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Id_klasy",
                table: "Klasy",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PrzedmiotyId_przedmiotu",
                table: "KlasaPrzedmiot",
                newName: "PrzedmiotyId");

            migrationBuilder.RenameColumn(
                name: "KlasyId_klasy",
                table: "KlasaPrzedmiot",
                newName: "KlasyId");

            migrationBuilder.RenameIndex(
                name: "IX_KlasaPrzedmiot_PrzedmiotyId_przedmiotu",
                table: "KlasaPrzedmiot",
                newName: "IX_KlasaPrzedmiot_PrzedmiotyId");

            migrationBuilder.AddColumn<int>(
                name: "KontoId",
                table: "Rodzice",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UczenId",
                table: "Rodzice",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Oceny",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "CzyWychowawca",
                table: "Nauczyciele",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "KontoId",
                table: "Nauczyciele",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrzelozonyId",
                table: "Nauczyciele",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WychowawcaId",
                table: "Klasy",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Konto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    HasloHash = table.Column<string>(type: "TEXT", nullable: false),
                    Rola = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ogloszenie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tytul = table.Column<string>(type: "TEXT", nullable: false),
                    Tresc = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogloszenie", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rodzice_KontoId",
                table: "Rodzice",
                column: "KontoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rodzice_UczenId",
                table: "Rodzice",
                column: "UczenId");

            migrationBuilder.CreateIndex(
                name: "IX_Nauczyciele_KontoId",
                table: "Nauczyciele",
                column: "KontoId");

            migrationBuilder.CreateIndex(
                name: "IX_Nauczyciele_PrzelozonyId",
                table: "Nauczyciele",
                column: "PrzelozonyId");

            migrationBuilder.CreateIndex(
                name: "IX_Klasy_WychowawcaId",
                table: "Klasy",
                column: "WychowawcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_KlasaPrzedmiot_Klasy_KlasyId",
                table: "KlasaPrzedmiot",
                column: "KlasyId",
                principalTable: "Klasy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KlasaPrzedmiot_Przedmioty_PrzedmiotyId",
                table: "KlasaPrzedmiot",
                column: "PrzedmiotyId",
                principalTable: "Przedmioty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Klasy_Nauczyciele_WychowawcaId",
                table: "Klasy",
                column: "WychowawcaId",
                principalTable: "Nauczyciele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nauczyciele_Konto_KontoId",
                table: "Nauczyciele",
                column: "KontoId",
                principalTable: "Konto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nauczyciele_Nauczyciele_PrzelozonyId",
                table: "Nauczyciele",
                column: "PrzelozonyId",
                principalTable: "Nauczyciele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Oceny_Przedmioty_PrzedmiotId",
                table: "Oceny",
                column: "PrzedmiotId",
                principalTable: "Przedmioty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Oceny_Uczniowie_UczenId",
                table: "Oceny",
                column: "UczenId",
                principalTable: "Uczniowie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rodzice_Konto_KontoId",
                table: "Rodzice",
                column: "KontoId",
                principalTable: "Konto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rodzice_Uczniowie_UczenId",
                table: "Rodzice",
                column: "UczenId",
                principalTable: "Uczniowie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Uczniowie_Klasy_KlasaId",
                table: "Uczniowie",
                column: "KlasaId",
                principalTable: "Klasy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Uczniowie_Konto_KontoId",
                table: "Uczniowie",
                column: "KontoId",
                principalTable: "Konto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KlasaPrzedmiot_Klasy_KlasyId",
                table: "KlasaPrzedmiot");

            migrationBuilder.DropForeignKey(
                name: "FK_KlasaPrzedmiot_Przedmioty_PrzedmiotyId",
                table: "KlasaPrzedmiot");

            migrationBuilder.DropForeignKey(
                name: "FK_Klasy_Nauczyciele_WychowawcaId",
                table: "Klasy");

            migrationBuilder.DropForeignKey(
                name: "FK_Nauczyciele_Konto_KontoId",
                table: "Nauczyciele");

            migrationBuilder.DropForeignKey(
                name: "FK_Nauczyciele_Nauczyciele_PrzelozonyId",
                table: "Nauczyciele");

            migrationBuilder.DropForeignKey(
                name: "FK_Oceny_Przedmioty_PrzedmiotId",
                table: "Oceny");

            migrationBuilder.DropForeignKey(
                name: "FK_Oceny_Uczniowie_UczenId",
                table: "Oceny");

            migrationBuilder.DropForeignKey(
                name: "FK_Rodzice_Konto_KontoId",
                table: "Rodzice");

            migrationBuilder.DropForeignKey(
                name: "FK_Rodzice_Uczniowie_UczenId",
                table: "Rodzice");

            migrationBuilder.DropForeignKey(
                name: "FK_Uczniowie_Klasy_KlasaId",
                table: "Uczniowie");

            migrationBuilder.DropForeignKey(
                name: "FK_Uczniowie_Konto_KontoId",
                table: "Uczniowie");

            migrationBuilder.DropTable(
                name: "Konto");

            migrationBuilder.DropTable(
                name: "Ogloszenie");

            migrationBuilder.DropIndex(
                name: "IX_Rodzice_KontoId",
                table: "Rodzice");

            migrationBuilder.DropIndex(
                name: "IX_Rodzice_UczenId",
                table: "Rodzice");

            migrationBuilder.DropIndex(
                name: "IX_Nauczyciele_KontoId",
                table: "Nauczyciele");

            migrationBuilder.DropIndex(
                name: "IX_Nauczyciele_PrzelozonyId",
                table: "Nauczyciele");

            migrationBuilder.DropIndex(
                name: "IX_Klasy_WychowawcaId",
                table: "Klasy");

            migrationBuilder.DropColumn(
                name: "KontoId",
                table: "Rodzice");

            migrationBuilder.DropColumn(
                name: "UczenId",
                table: "Rodzice");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Oceny");

            migrationBuilder.DropColumn(
                name: "CzyWychowawca",
                table: "Nauczyciele");

            migrationBuilder.DropColumn(
                name: "KontoId",
                table: "Nauczyciele");

            migrationBuilder.DropColumn(
                name: "PrzelozonyId",
                table: "Nauczyciele");

            migrationBuilder.DropColumn(
                name: "WychowawcaId",
                table: "Klasy");

            migrationBuilder.RenameColumn(
                name: "KontoId",
                table: "Uczniowie",
                newName: "Id_wychowawcy");

            migrationBuilder.RenameColumn(
                name: "KlasaId",
                table: "Uczniowie",
                newName: "Id_rodzica");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Uczniowie",
                newName: "Id_Ucznia");

            migrationBuilder.RenameIndex(
                name: "IX_Uczniowie_KontoId",
                table: "Uczniowie",
                newName: "IX_Uczniowie_Id_wychowawcy");

            migrationBuilder.RenameIndex(
                name: "IX_Uczniowie_KlasaId",
                table: "Uczniowie",
                newName: "IX_Uczniowie_Id_rodzica");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Rodzice",
                newName: "Id_rodzica");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "Przedmioty",
                newName: "NazwaPrzedmiotu");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Przedmioty",
                newName: "Id_przedmiotu");

            migrationBuilder.RenameColumn(
                name: "UczenId",
                table: "Oceny",
                newName: "Id_przedmiotu");

            migrationBuilder.RenameColumn(
                name: "PrzedmiotId",
                table: "Oceny",
                newName: "Id_nauczyciela");

            migrationBuilder.RenameColumn(
                name: "Opis",
                table: "Oceny",
                newName: "DataWpisu");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Oceny",
                newName: "Id_oceny");

            migrationBuilder.RenameIndex(
                name: "IX_Oceny_UczenId",
                table: "Oceny",
                newName: "IX_Oceny_Id_przedmiotu");

            migrationBuilder.RenameIndex(
                name: "IX_Oceny_PrzedmiotId",
                table: "Oceny",
                newName: "IX_Oceny_Id_nauczyciela");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Nauczyciele",
                newName: "Id_wychowawcy");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Klasy",
                newName: "Id_klasy");

            migrationBuilder.RenameColumn(
                name: "PrzedmiotyId",
                table: "KlasaPrzedmiot",
                newName: "PrzedmiotyId_przedmiotu");

            migrationBuilder.RenameColumn(
                name: "KlasyId",
                table: "KlasaPrzedmiot",
                newName: "KlasyId_klasy");

            migrationBuilder.RenameIndex(
                name: "IX_KlasaPrzedmiot_PrzedmiotyId",
                table: "KlasaPrzedmiot",
                newName: "IX_KlasaPrzedmiot_PrzedmiotyId_przedmiotu");

            migrationBuilder.AddColumn<int>(
                name: "Id_klasy",
                table: "Uczniowie",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id_nauczyciela",
                table: "Przedmioty",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id_Ucznia",
                table: "Oceny",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Uczniowie_Id_klasy",
                table: "Uczniowie",
                column: "Id_klasy");

            migrationBuilder.CreateIndex(
                name: "IX_Przedmioty_Id_nauczyciela",
                table: "Przedmioty",
                column: "Id_nauczyciela");

            migrationBuilder.CreateIndex(
                name: "IX_Oceny_Id_Ucznia",
                table: "Oceny",
                column: "Id_Ucznia");

            migrationBuilder.AddForeignKey(
                name: "FK_KlasaPrzedmiot_Klasy_KlasyId_klasy",
                table: "KlasaPrzedmiot",
                column: "KlasyId_klasy",
                principalTable: "Klasy",
                principalColumn: "Id_klasy",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KlasaPrzedmiot_Przedmioty_PrzedmiotyId_przedmiotu",
                table: "KlasaPrzedmiot",
                column: "PrzedmiotyId_przedmiotu",
                principalTable: "Przedmioty",
                principalColumn: "Id_przedmiotu",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Oceny_Nauczyciele_Id_nauczyciela",
                table: "Oceny",
                column: "Id_nauczyciela",
                principalTable: "Nauczyciele",
                principalColumn: "Id_wychowawcy",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Oceny_Przedmioty_Id_przedmiotu",
                table: "Oceny",
                column: "Id_przedmiotu",
                principalTable: "Przedmioty",
                principalColumn: "Id_przedmiotu",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Oceny_Uczniowie_Id_Ucznia",
                table: "Oceny",
                column: "Id_Ucznia",
                principalTable: "Uczniowie",
                principalColumn: "Id_Ucznia",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmioty_Nauczyciele_Id_nauczyciela",
                table: "Przedmioty",
                column: "Id_nauczyciela",
                principalTable: "Nauczyciele",
                principalColumn: "Id_wychowawcy",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Uczniowie_Klasy_Id_klasy",
                table: "Uczniowie",
                column: "Id_klasy",
                principalTable: "Klasy",
                principalColumn: "Id_klasy",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Uczniowie_Nauczyciele_Id_wychowawcy",
                table: "Uczniowie",
                column: "Id_wychowawcy",
                principalTable: "Nauczyciele",
                principalColumn: "Id_wychowawcy",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Uczniowie_Rodzice_Id_rodzica",
                table: "Uczniowie",
                column: "Id_rodzica",
                principalTable: "Rodzice",
                principalColumn: "Id_rodzica",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
