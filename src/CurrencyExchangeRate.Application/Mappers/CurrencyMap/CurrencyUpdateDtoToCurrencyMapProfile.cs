using AutoMapper;
using CurrencyExchangeRate.Application.DTOs;
using CurrencyExchangeRate.Domain.Entities;

namespace CurrencyExchangeRate.Application.Mappers
{
    public class CurrencyUpdateDtoToCurrencyMapProfile : Profile
    {
        public CurrencyUpdateDtoToCurrencyMapProfile()
        {
            CreateMap<CurrencyUpdateDto, Currency>()
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.CurrencyCode))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CurrencyName));
        }
    }
}