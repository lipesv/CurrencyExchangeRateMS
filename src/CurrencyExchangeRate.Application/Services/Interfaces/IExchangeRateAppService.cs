using CurrencyExchangeRate.Application.DTOs;

namespace CurrencyExchangeRate.Application.Services.Interfaces
{
    public interface IExchangeRateAppService
    {
        /// <summary>
        /// Gets the exchange rate for a given base and target currency code.
        /// </summary>
        /// <param name="baseCurrencyCode">The base currency code.</param>
        /// <param name="targetCurrencyCode">The target currency code.</param>
        /// <returns>The exchange rate between the base and target currency.</returns>
        Task<ExchangeRateDto?> GetExchangeRate(string baseCurrencyCode, string targetCurrencyCode);

        /// <summary>Creates a new exchange rate.</summary>
        /// <param name="exchangeRateCreateDto">The exchange rate create DTO.</param>
        /// <returns>The created exchange rate.</returns>
        Task<ExchangeRateDto?> CreateExchangeRate(ExchangeRateCreateDto exchangeRateCreateDto);

        /// <summary>Updates the exchange rate.</summary>
        /// <param name="exchangeRateUpdateDto">The exchange rate update DTO.</param>
        /// <returns>The updated exchange rate.</returns>
        Task<ExchangeRateDto?> UpdateExchangeRate(string baseCurrencyCode,
                                                  string targetCurrencyCode,
                                                  ExchangeRateUpdateDto exchangeRateUpdateDto);
    }
}