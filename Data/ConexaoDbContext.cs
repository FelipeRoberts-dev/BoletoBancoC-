using Microsoft.EntityFrameworkCore;
using TesteBanco.Models;

namespace TesteBanco.Data
{
    public class ConexaoDbContext : DbContext
    {
        public ConexaoDbContext(DbContextOptions<ConexaoDbContext> options) : base(options)
        {
        }

        public DbSet<Boleto> Boletos { get; set; }
        public DbSet<Banco> Bancos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamento de entidades

            modelBuilder.Entity<Banco>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.NomeBanco).IsRequired();
                entity.Property(b => b.CodigoBanco).IsRequired();
                entity.Property(b => b.PercentualJuros).IsRequired();
            });

            modelBuilder.Entity<Boleto>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.NomePagador).IsRequired();
                entity.Property(b => b.CPFCNPJPagador).IsRequired();
                entity.Property(b => b.NomeBeneficiario).IsRequired();
                entity.Property(b => b.CPFCNPJBeneficiario).IsRequired();
                entity.Property(b => b.Valor).IsRequired();
                entity.Property(b => b.DataVencimento).IsRequired();
                entity.Property(b => b.BancoId).IsRequired();

                // Defina o relacionamento entre Boleto e Banco
                modelBuilder.Entity<Boleto>()
                   .HasOne(b => b.Banco)
                   .WithOne(); // Sem especificar a propriedade de navegação no Banco

            });
        }
    }
}

