using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeBooks.Data.Migrations
{
    public partial class UpdateModelsAnunciosOfertas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataLancamento",
                table: "Ofertas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Ofertas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Montante",
                table: "Ofertas",
                type: "decimal(18,2)",
                maxLength: 100,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "Anuncios",
                type: "decimal(18,2)",
                maxLength: 100,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataLancamento",
                table: "Ofertas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Ofertas");

            migrationBuilder.DropColumn(
                name: "Montante",
                table: "Ofertas");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Anuncios");
        }
    }
}
