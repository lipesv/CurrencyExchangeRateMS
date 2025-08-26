using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeRate.Infrastructure.Persistence.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitialiseDatabaseAsync(AppDbContext context)
        {
            await SeedAsync(context);
        }

        private static async Task SeedAsync(AppDbContext context)
        {
            // Verifica se já existem moedas
            if (await context.Currencies.AnyAsync())
            {
                return; // Banco de dados já foi populado
            }

            var currencies = new[]
            {
                Currency.Create("USD", "US Dollar"),
                Currency.Create("EUR", "Euro"),
                Currency.Create("BRL", "Brazilian Real"),
            };

            await context.Currencies.AddRangeAsync(currencies);

            var exchangeRates = new[]
            {
                ExchangeRate.Create("BRL", "USD", 0.183m),
                ExchangeRate.Create("USD", "BRL", 5.47m),
                ExchangeRate.Create("USD", "EUR", 0.853m),
                ExchangeRate.Create("EUR", "USD", 1.17m),
                ExchangeRate.Create("BRL", "EUR", 0.157m),
                ExchangeRate.Create("EUR", "BRL", 6.35m)
            };

            await context.ExchangeRates.AddRangeAsync(exchangeRates);

            await context.SaveChangesAsync();
        }
    }
}