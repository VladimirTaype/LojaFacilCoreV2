using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LojaFacilCoreV2.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Define precisão para todos os campos decimais (2 casas)
            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ItemVenda>()
                .Property(iv => iv.PrecoUnitario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ItemVenda>()
                .Property(iv => iv.Subtotal)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Venda>()
                .Property(v => v.Total)
                .HasColumnType("decimal(18,2)");

            // 🔹 Relação 1:N entre Venda e ItensVenda
            modelBuilder.Entity<ItemVenda>()
                .HasOne(iv => iv.Venda)
                .WithMany(v => v.Itens)
                .HasForeignKey(iv => iv.VendaId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Relação 1:N entre Produto e ItensVenda (opcional, mas recomendado)
            modelBuilder.Entity<ItemVenda>()
                .HasOne(iv => iv.Produto)
                .WithMany()
                .HasForeignKey(iv => iv.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
