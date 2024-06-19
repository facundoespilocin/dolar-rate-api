using AutoMapper;
using DollarInfo.Services.Models;

namespace DollarInfo.Services.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DollarRatesResponse, DollarRatesDto>();
        }
    }
}