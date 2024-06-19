using AutoMapper;
using Ecommerce.Services.Models;

namespace Ecommerce.Services.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DollarRatesResponse, DollarRatesDto>();
        }
    }
}