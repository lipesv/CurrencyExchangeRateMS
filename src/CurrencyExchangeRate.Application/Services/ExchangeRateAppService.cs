using AutoMapper;
using CurrencyExchangeRate.Application.DTOs;
using CurrencyExchangeRate.Application.Services.Interfaces;
using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Domain.Interfaces;

namespace CurrencyExchangeRate.Application.Services
{
    public class ExchangeRateAppService : IExchangeRateAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExchangeRateAppService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ExchangeRateDto?> GetExchangeRate(string baseCurrencyCode,
                                                            string targetCurrencyCode)
        {
            var exchangeRate = await _unitOfWork.exchangeRateRepository.Get(baseCurrencyCode,
                                                                            targetCurrencyCode);

            if (exchangeRate == null)
                return null;

            var exchangeRateDto = _mapper.Map<ExchangeRateDto>(exchangeRate);

            return exchangeRateDto;
        }

        public async Task<ExchangeRateDto?> CreateExchangeRate(ExchangeRateCreateDto exchangeRateCreateDto)
        {
            var existingRate = await _unitOfWork.exchangeRateRepository.Get(exchangeRateCreateDto.BaseCurrencyCode,
                                                                            exchangeRateCreateDto.TargetCurrencyCode);

            if (existingRate != null)
                return null;

            var exchangeRate = ExchangeRate.Create(exchangeRateCreateDto.BaseCurrencyCode,
                                                   exchangeRateCreateDto.TargetCurrencyCode,
                                                   exchangeRateCreateDto.Rate);

            await _unitOfWork.exchangeRateRepository.Add(exchangeRate);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<ExchangeRateDto>(exchangeRate);
        }

        public async Task<ExchangeRateDto?> UpdateExchangeRate(string baseCurrencyCode,
                                                               string targetCurrencyCode,
                                                               ExchangeRateUpdateDto exchangeRateUpdateDto)
        {
            var existingExchangeRate = await _unitOfWork.exchangeRateRepository.Get(baseCurrencyCode,
                                                                                    targetCurrencyCode);

            if (existingExchangeRate == null)
                return null;

            existingExchangeRate.UpdateRate(exchangeRateUpdateDto.Rate);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<ExchangeRateDto>(existingExchangeRate);
        }
    }
}