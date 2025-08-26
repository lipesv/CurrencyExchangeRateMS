using CurrencyExchangeRate.Domain.Interfaces;
using CurrencyExchangeRate.Domain.Interfaces.Repositories;
using CurrencyExchangeRate.Infrastructure.Persistence;
using CurrencyExchangeRate.Infrastructure.Persistence.DataContext;
using CurrencyExchangeRate.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchangeRate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("CurrencyExchangeRateDB"));

            return services;
        }
    }
}