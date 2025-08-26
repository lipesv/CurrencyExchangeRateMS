using AutoMapper;
using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Application.DTOs;

namespace CurrencyExchangeRate.Application.Mappers.ExchangeRateMap
{
    public class ExchangeRateToExchangeRateDtoMapProfile : Profile
    {
        public ExchangeRateToExchangeRateDtoMapProfile()
        {

            CreateMap<ExchangeRate, ExchangeRateDto>()
                .ForMember(dest => dest.ExchangeRateId, opt => opt.MapFrom(src => src.ExchangeRateId))
                .ForMember(dest => dest.BaseCurrencyCode, opt => opt.MapFrom(src => src.BaseCurrencyCode))
                .ForMember(dest => dest.TargetCurrencyCode, opt => opt.MapFrom(src => src.TargetCurrencyCode))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
                .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.LastUpdated));
        }
    }

}