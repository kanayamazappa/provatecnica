using Microsoft.EntityFrameworkCore;
using ProvaTecnicaApi.Service.Models;

namespace ProvaTecnicaApi.Service.Data
{
    public class ProvaTecnicaApiContext : DbContext
    {
        public ProvaTecnicaApiContext(DbContextOptions<ProvaTecnicaApiContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Categoria>().HasKey(c => c.IdCategoria);
            builder.Entity<Produto>().HasKey(p => p.IdProduto);
            builder.Entity<Usuario>().HasKey(u => u.IdUsuario);

            base.OnModelCreating(builder);
        }
    }
}
