using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Domain.Interfaces.Repositories;
using CurrencyExchangeRate.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeRate.Infrastructure.Persistence.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _context;

        public CurrencyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Currency>> GetAll()
        {
            return await _context.Currencies.ToListAsync();
        }

        public async Task<Currency?> GetByCode(string currencyCode)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.Code.Equals(currencyCode,
                                                                                    StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Currency> Add(Currency currency)
        {
            await _context.Currencies.AddAsync(currency);
            return currency;
        }

        public async Task<IEnumerable<Currency>> AddRange(IEnumerable<Currency> currencies)
        {
            await _context.Currencies.AddRangeAsync(currencies);
            return currencies;
        }


    }
}