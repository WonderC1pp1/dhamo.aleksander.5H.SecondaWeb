using Microsoft.EntityFrameworkCore.Migrations;

namespace dhamo.aleksander._5H.SecondaWeb.Migrations
{
    public partial class SistematatabellaImmagini : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Immagini",
                newName: "linkImmagine");

            migrationBuilder.RenameColumn(
                name: "Immagine",
                table: "Immagini",
                newName: "idUtente");

            migrationBuilder.AddColumn<string>(
                name: "Descrizione",
                table: "Immagini",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titolo",
                table: "Immagini",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descrizione",
                table: "Immagini");

            migrationBuilder.DropColumn(
                name: "Titolo",
                table: "Immagini");

            migrationBuilder.RenameColumn(
                name: "linkImmagine",
                table: "Immagini",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "idUtente",
                table: "Immagini",
                newName: "Immagine");
        }
    }
}
