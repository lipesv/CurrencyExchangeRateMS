
using CurrencyExchangeRate.Application.Mappers.CurrencyMap;
using CurrencyExchangeRate.Application.Services;
using CurrencyExchangeRate.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchangeRate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CurrencyToCurrencyDtoMapProfile).Assembly);
            services.AddScoped<IExchangeRateAppService, ExchangeRateAppService>();
            services.AddScoped<ICurrencyAppService, CurrencyAppService>();

            return services;
        }
    }
}