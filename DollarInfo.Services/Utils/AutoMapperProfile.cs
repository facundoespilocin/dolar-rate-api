using AutoMapper;
using DollarInfo.DAL.Dtos.Indexes;
using DollarInfo.DAL.Dtos.Rates;
using DollarInfo.DAL.Models;
using DollarInfo.Services.Models;
using static DollarInfo.Utils.Enums;

namespace DollarInfo.Services.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DollarRatesResponse, ExchangeRateValues>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (long)(ExchangeRateType)Enum.Parse(typeof(ExchangeRateType), src.Name.Replace(" ", ""), true)))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.ShortName, opt => opt.MapFrom(src => src.ShortName))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PurchasePrice, opt => opt.MapFrom(src => src.PurchasePrice))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

            CreateMap<DollarRatesResponse, DollarRatesDto>();

            CreateMap<FixedTermRateResponse, FixedTermRateDto>()
                .ForMember(dest => dest.NoClientsTna, opt => opt.MapFrom(src => Math.Round(src.ClientsTna * 100, 2)))
                .ForMember(dest => dest.ClientsTna, opt => opt.MapFrom(src => Math.Round(src.ClientsTna * 100, 2)));

            CreateMap<InflationIndexResponse, InflationIndexDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value.HasValue ? src.Value : 0));
        }
    }
}