using FreeBooks.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreeBooks.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // adicionar tabelas a Base de Dados
        public DbSet<Utilizadores> Utilizadores { get; set; }
        public DbSet<Fotos> Fotos { get; set; }
        public DbSet<Livros> Livros { get; set; }
        public DbSet<Ofertas> Ofertas { get; set; }
        public DbSet<Anuncios> Anuncios { get; set; }
        public DbSet<Transacoes> Transacoes { get; set; }

    }
}