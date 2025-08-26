using CurrencyExchangeRate.Domain.Entities;

namespace CurrencyExchangeRate.Domain.Interfaces.Repositories
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetAll();
        Task<Currency?> GetByCode(string currencyCode);
        Task<Currency> Add(Currency currency);
        Task<IEnumerable<Currency>> AddRange(IEnumerable<Currency> currencies);
    }
}