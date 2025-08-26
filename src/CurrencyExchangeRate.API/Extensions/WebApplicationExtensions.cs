using CurrencyExchangeRate.Infrastructure.Persistence.DataContext;
using CurrencyExchangeRate.Infrastructure.Persistence.Extensions;

namespace CurrencyExchangeRate.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await DatabaseExtensions.InitialiseDatabaseAsync(context);
            }
            catch (Exception ex)
            {
                var logger = app.Services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB.");

                throw;
            }
        }
    }
}