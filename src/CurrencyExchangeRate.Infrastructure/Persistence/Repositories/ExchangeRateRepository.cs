using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Domain.Interfaces.Repositories;
using CurrencyExchangeRate.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeRate.Infrastructure.Persistence.Repositories
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly AppDbContext _context;

        public ExchangeRateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ExchangeRate?> Get(string baseCurrencyCode, string targetCurrencyCode)
        {
            return await _context.ExchangeRates.FirstOrDefaultAsync(er => er.BaseCurrencyCode.Equals(baseCurrencyCode,
                                                                                                     StringComparison.OrdinalIgnoreCase)
                                                                          && er.TargetCurrencyCode.Equals(targetCurrencyCode,
                                                                                                          StringComparison.OrdinalIgnoreCase));
        }

        public async Task<ExchangeRate> Add(ExchangeRate exchangeRateCreate)
        {
            await _context.ExchangeRates.AddAsync(exchangeRateCreate);
            return exchangeRateCreate;
        }
    }
}