using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeBooks.Data.Migrations
{
    public partial class controllersCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Galerias_GaleriaFk",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_GaleriaFk",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "GaleriaFk",
                table: "Livros");

            migrationBuilder.AddColumn<int>(
                name: "LivroFk",
                table: "Galerias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Galerias_LivroFk",
                table: "Galerias",
                column: "LivroFk");

            migrationBuilder.AddForeignKey(
                name: "FK_Galerias_Livros_LivroFk",
                table: "Galerias",
                column: "LivroFk",
                principalTable: "Livros",
                principalColumn: "IdLivro",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galerias_Livros_LivroFk",
                table: "Galerias");

            migrationBuilder.DropIndex(
                name: "IX_Galerias_LivroFk",
                table: "Galerias");

            migrationBuilder.DropColumn(
                name: "LivroFk",
                table: "Galerias");

            migrationBuilder.AddColumn<int>(
                name: "GaleriaFk",
                table: "Livros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Livros_GaleriaFk",
                table: "Livros",
                column: "GaleriaFk");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Galerias_GaleriaFk",
                table: "Livros",
                column: "GaleriaFk",
                principalTable: "Galerias",
                principalColumn: "IdGaleria",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
