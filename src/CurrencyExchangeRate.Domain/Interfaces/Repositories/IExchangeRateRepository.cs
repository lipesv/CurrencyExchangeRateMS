using CurrencyExchangeRate.Domain.Entities;

namespace CurrencyExchangeRate.Domain.Interfaces.Repositories
{
    public interface IExchangeRateRepository
    {
        /// <summary>
        /// Gets the exchange rate for a given base and target currency code.
        /// </summary>
        /// <param name="baseCurrencyCode">The base currency code.</param>
        /// <param name="targetCurrencyCode">The target currency code.</param>
        /// <returns>The exchange rate between the base and target currency.</returns>
        Task<ExchangeRate?> Get(string baseCurrencyCode, string targetCurrencyCode);

        /// <summary>
        /// Adds a new exchange rate.
        /// </summary>
        /// <param name="exchangeRateCreate">The exchange rate to add.</param>
        /// <returns>True if the exchange rate was added successfully; otherwise, false.</returns>
        Task<ExchangeRate> Add(ExchangeRate exchangeRateCreate);
    }
}