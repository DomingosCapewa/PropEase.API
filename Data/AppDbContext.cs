using Microsoft.EntityFrameworkCore;
using PropEase.API.Models;

namespace PropEase.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Imovel> Imoveis => Set<Imovel>();
        public DbSet<Casa> Casas => Set<Casa>();
        public DbSet<Apartamento> Apartamentos => Set<Apartamento>();
        public DbSet<Proprietario> Proprietarios => Set<Proprietario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Imovel>()
                .HasDiscriminator<string>("TipoImovel")
                .HasValue<Casa>("Casa")
                .HasValue<Apartamento>("Apartamento");

            modelBuilder.Entity<Imovel>()
                .HasOne(i => i.Proprietario)
                .WithMany()
                .HasForeignKey(i => i.ProprietarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
