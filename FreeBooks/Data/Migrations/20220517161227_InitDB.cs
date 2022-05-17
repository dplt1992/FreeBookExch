using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeBooks.Data.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Galerias",
                columns: table => new
                {
                    IdGaleria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galerias", x => x.IdGaleria);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    IdTransacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataTrasancao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.IdTransacao);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoUtilizador = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    IdFoto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GaleriaFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.IdFoto);
                    table.ForeignKey(
                        name: "FK_Fotos_Galerias_GaleriaFk",
                        column: x => x.GaleriaFk,
                        principalTable: "Galerias",
                        principalColumn: "IdGaleria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anuncios",
                columns: table => new
                {
                    IdAnuncio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UtilizadorFk = table.Column<int>(type: "int", nullable: false),
                    TransacaoFk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anuncios", x => x.IdAnuncio);
                    table.ForeignKey(
                        name: "FK_Anuncios_Transacoes_TransacaoFk",
                        column: x => x.TransacaoFk,
                        principalTable: "Transacoes",
                        principalColumn: "IdTransacao");
                    table.ForeignKey(
                        name: "FK_Anuncios_Utilizadores_UtilizadorFk",
                        column: x => x.UtilizadorFk,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ofertas",
                columns: table => new
                {
                    IdOferta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AnuncioFk = table.Column<int>(type: "int", nullable: false),
                    UtilizadorFk = table.Column<int>(type: "int", nullable: false),
                    TransacaoFk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ofertas", x => x.IdOferta);
                    table.ForeignKey(
                        name: "FK_Ofertas_Anuncios_AnuncioFk",
                        column: x => x.AnuncioFk,
                        principalTable: "Anuncios",
                        principalColumn: "IdAnuncio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ofertas_Transacoes_TransacaoFk",
                        column: x => x.TransacaoFk,
                        principalTable: "Transacoes",
                        principalColumn: "IdTransacao");
                    table.ForeignKey(
                        name: "FK_Ofertas_Utilizadores_UtilizadorFk",
                        column: x => x.UtilizadorFk,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    IdLivro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Edicao = table.Column<int>(type: "int", nullable: false),
                    Editora = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnuncioFK = table.Column<int>(type: "int", nullable: true),
                    OfertaFK = table.Column<int>(type: "int", nullable: true),
                    GaleriaFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.IdLivro);
                    table.ForeignKey(
                        name: "FK_Livros_Anuncios_AnuncioFK",
                        column: x => x.AnuncioFK,
                        principalTable: "Anuncios",
                        principalColumn: "IdAnuncio");
                    table.ForeignKey(
                        name: "FK_Livros_Galerias_GaleriaFk",
                        column: x => x.GaleriaFk,
                        principalTable: "Galerias",
                        principalColumn: "IdGaleria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livros_Ofertas_OfertaFK",
                        column: x => x.OfertaFK,
                        principalTable: "Ofertas",
                        principalColumn: "IdOferta");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anuncios_TransacaoFk",
                table: "Anuncios",
                column: "TransacaoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Anuncios_UtilizadorFk",
                table: "Anuncios",
                column: "UtilizadorFk");

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_GaleriaFk",
                table: "Fotos",
                column: "GaleriaFk");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_AnuncioFK",
                table: "Livros",
                column: "AnuncioFK");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_GaleriaFk",
                table: "Livros",
                column: "GaleriaFk");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_OfertaFK",
                table: "Livros",
                column: "OfertaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_AnuncioFk",
                table: "Ofertas",
                column: "AnuncioFk");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_TransacaoFk",
                table: "Ofertas",
                column: "TransacaoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_UtilizadorFk",
                table: "Ofertas",
                column: "UtilizadorFk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "Galerias");

            migrationBuilder.DropTable(
                name: "Ofertas");

            migrationBuilder.DropTable(
                name: "Anuncios");

            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Utilizadores");
        }
    }
}
