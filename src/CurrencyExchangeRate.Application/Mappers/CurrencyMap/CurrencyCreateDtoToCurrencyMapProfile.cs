using AutoMapper;
using CurrencyExchangeRate.Application.DTOs;
using CurrencyExchangeRate.Domain.Entities;

namespace CurrencyExchangeRate.Application.Mappers.CurrencyMap
{
    public class CurrencyCreateDtoToCurrencyMapProfile : Profile
    {
        public CurrencyCreateDtoToCurrencyMapProfile()
        {
            CreateMap<CurrencyCreateDto, Currency>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CurrencyName))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.CurrencyCode));
        }
    }
}