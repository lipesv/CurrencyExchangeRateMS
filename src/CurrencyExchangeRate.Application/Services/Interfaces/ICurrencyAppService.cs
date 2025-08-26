using CurrencyExchangeRate.Application.DTOs;

namespace CurrencyExchangeRate.Application.Services.Interfaces
{
    public interface ICurrencyAppService
    {
        Task<List<CurrencyDto>> GetAllCurrencies();
        Task<CurrencyDto?> GetCurrencyByCode(string currencyCode);
        Task<CurrencyDto?> CreateCurrency(CurrencyCreateDto currencyCreateDto);
        Task<List<CurrencyDto>> CreateCurrencies(List<CurrencyCreateDto> currencyCreateDtos);
        Task<CurrencyDto?> UpdateCurrency(string currencyCode, CurrencyUpdateDto currencyUpdateDto);

    }
}