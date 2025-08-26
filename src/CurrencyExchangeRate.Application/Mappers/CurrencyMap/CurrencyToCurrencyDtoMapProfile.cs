using AutoMapper;
using CurrencyExchangeRate.Application.DTOs;
using CurrencyExchangeRate.Domain.Entities;

namespace CurrencyExchangeRate.Application.Mappers.CurrencyMap
{
    public class CurrencyToCurrencyDtoMapProfile : Profile
    {
        public CurrencyToCurrencyDtoMapProfile()
        {
            CreateMap<Currency, CurrencyDto>()
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId))
                .ForMember(dest => dest.CurrencyName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Code));
        }
    }
}