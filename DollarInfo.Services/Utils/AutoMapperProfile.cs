using AutoMapper;
using DollarInfo.DAL.Dtos.Rates;
using DollarInfo.Services.Models;
using DollarInfo.Utils.Extensions;

namespace DollarInfo.Services.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DollarRatesResponse, DollarRatesDto>();
            CreateMap<FixedTermRateResponse, FixedTermRateDto>()
                .ForMember(dest => dest.NoClientsTna, opt => opt
                .MapFrom(src => src.NoClientsTna ?? src.ClientsTna));
        }
    }
}