using AutoMapper;
using CurrencyExchangeRate.Application.DTOs;
using CurrencyExchangeRate.Application.Services.Interfaces;
using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Domain.Interfaces;

namespace CurrencyExchangeRate.Application.Services
{
    public class CurrencyAppService : ICurrencyAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CurrencyAppService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CurrencyDto>> GetAllCurrencies()
        {
            var currencies = await _unitOfWork.currencyRepository.GetAll();
            return _mapper.Map<List<CurrencyDto>>(currencies);
        }

        public async Task<CurrencyDto?> GetCurrencyByCode(string currencyCode)
        {
            var currency = await _unitOfWork.currencyRepository.GetByCode(currencyCode);

            if (currency == null)
            {
                return null;
            }

            return _mapper.Map<CurrencyDto>(currency);
        }

        public async Task<CurrencyDto?> CreateCurrency(CurrencyCreateDto currencyCreateDto)
        {
            var existingCurrency = await _unitOfWork.currencyRepository.GetByCode(currencyCreateDto.CurrencyCode);

            if (existingCurrency != null)
            {
                return null;
            }

            var currency = Currency.Create(currencyCreateDto.CurrencyCode, currencyCreateDto.CurrencyName);

            await _unitOfWork.currencyRepository.Add(currency);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CurrencyDto>(currency);
        }

        public async Task<List<CurrencyDto>> CreateCurrencies(List<CurrencyCreateDto> currencyCreateDtos)
        {
            var newCurrencies = new List<Currency>();

            foreach (var dto in currencyCreateDtos)
            {
                var existingCurrency = await _unitOfWork.currencyRepository.GetByCode(dto.CurrencyCode);

                if (existingCurrency == null)
                {
                    newCurrencies.Add(Currency.Create(dto.CurrencyCode, dto.CurrencyName));
                }
            }

            if (!newCurrencies.Any())
            {
                return new List<CurrencyDto>();
            }

            await _unitOfWork.currencyRepository.AddRange(newCurrencies);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<List<CurrencyDto>>(newCurrencies);
        }

        public async Task<CurrencyDto?> UpdateCurrency(string currencyCode, CurrencyUpdateDto currencyUpdateDto)
        {
            var existingCurrency = await _unitOfWork.currencyRepository.GetByCode(currencyCode);

            if (existingCurrency == null)
            {
                return null;
            }

            existingCurrency.UpdateCurrency(currencyUpdateDto.CurrencyCode, currencyUpdateDto.CurrencyName);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<CurrencyDto>(existingCurrency);
        }


    }
}