using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeBooks.Data.Migrations
{
    public partial class modelsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galerias_Livros_LivroFk",
                table: "Galerias");

            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Galerias_GaleriaFK",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_GaleriaFK",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "GaleriaFK",
                table: "Livros");

            migrationBuilder.RenameColumn(
                name: "LivroFk",
                table: "Galerias",
                newName: "LivroFK");

            migrationBuilder.RenameIndex(
                name: "IX_Galerias_LivroFk",
                table: "Galerias",
                newName: "IX_Galerias_LivroFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Galerias_Livros_LivroFK",
                table: "Galerias",
                column: "LivroFK",
                principalTable: "Livros",
                principalColumn: "IdLivro",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galerias_Livros_LivroFK",
                table: "Galerias");

            migrationBuilder.RenameColumn(
                name: "LivroFK",
                table: "Galerias",
                newName: "LivroFk");

            migrationBuilder.RenameIndex(
                name: "IX_Galerias_LivroFK",
                table: "Galerias",
                newName: "IX_Galerias_LivroFk");

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
                name: "FK_Galerias_Livros_LivroFk",
                table: "Galerias",
                column: "LivroFk",
                principalTable: "Livros",
                principalColumn: "IdLivro",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Galerias_GaleriaFK",
                table: "Livros",
                column: "GaleriaFK",
                principalTable: "Galerias",
                principalColumn: "IdGaleria",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
