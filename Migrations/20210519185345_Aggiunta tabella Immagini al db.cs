using Microsoft.EntityFrameworkCore.Migrations;

namespace dhamo.aleksander._5H.SecondaWeb.Migrations
{
    public partial class AggiuntatabellaImmaginialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Immagini",
                columns: table => new
                {
                    idImage = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Immagine = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Immagini", x => x.idImage);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Immagini");
        }
    }
}
