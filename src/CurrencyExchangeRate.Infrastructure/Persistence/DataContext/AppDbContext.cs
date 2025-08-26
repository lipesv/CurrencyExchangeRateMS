using System.Reflection;
using CurrencyExchangeRate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeRate.Infrastructure.Persistence.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Este comando varre o assembly atual (CurrencyExchangeRate.Infrastructure)
            // em busca de todas as classes que implementam IEntityTypeConfiguration
            // e as aplica automaticamente.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}