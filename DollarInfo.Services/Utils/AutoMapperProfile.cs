﻿using AutoMapper;
using DollarInfo.DAL.Dtos.Indexes;
using DollarInfo.DAL.Dtos.Rates;
using DollarInfo.Services.Models;

namespace DollarInfo.Services.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
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