using AutoMapper;
using CurrencyExchangeRate.Application.DTOs;
using CurrencyExchangeRate.Domain.Entities;

namespace CurrencyExchangeRate.Application.Mappers.ExchangeRateMap
{
    public class ExchangeRateUpdateDtoToExchangeRate : Profile
    {
        public ExchangeRateUpdateDtoToExchangeRate()
        {
            CreateMap<ExchangeRateUpdateDto, ExchangeRate>()
                .ForMember(dest => dest.ExchangeRateId, opt => opt.MapFrom(src => src.ExchangeRateId))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate));
        }
    }
}