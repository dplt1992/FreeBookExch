using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeBooks.Data.Migrations
{
    public partial class relationsCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GaleriaFK",
                table: "Livros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Livros_GaleriaFK",
                table: "Livros",
                column: "GaleriaFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Galerias_GaleriaFK",
                table: "Livros",
                column: "GaleriaFK",
                principalTable: "Galerias",
                principalColumn: "IdGaleria",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Galerias_GaleriaFK",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_GaleriaFK",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "GaleriaFK",
                table: "Livros");
        }
    }
}
