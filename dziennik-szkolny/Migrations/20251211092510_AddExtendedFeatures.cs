using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dziennik_szkolny.Migrations
{
    /// <inheritdoc />
    public partial class AddExtendedFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Uczniowie",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Uczniowie",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Rodzice",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Rodzice",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NauczycielId",
                table: "Przedmioty",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrescKsztalcenia",
                table: "Przedmioty",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Nauczyciele",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Nauczyciele",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Przedmioty_NauczycielId",
                table: "Przedmioty",
                column: "NauczycielId");

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmioty_Nauczyciele_NauczycielId",
                table: "Przedmioty",
                column: "NauczycielId",
                principalTable: "Nauczyciele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Przedmioty_Nauczyciele_NauczycielId",
                table: "Przedmioty");

            migrationBuilder.DropIndex(
                name: "IX_Przedmioty_NauczycielId",
                table: "Przedmioty");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Uczniowie");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Uczniowie");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Rodzice");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Rodzice");

            migrationBuilder.DropColumn(
                name: "NauczycielId",
                table: "Przedmioty");

            migrationBuilder.DropColumn(
                name: "TrescKsztalcenia",
                table: "Przedmioty");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Nauczyciele");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Nauczyciele");
        }
    }
}
