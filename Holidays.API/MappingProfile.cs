using AutoMapper;
using Holidays.API.DTOs;
using Holidays.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Holiday, HolidayCreateDto>();
            CreateMap<HolidayCreateDto, Holiday>();

            CreateMap<Holiday, HolidayUpdateDto>();
            CreateMap<HolidayUpdateDto, Holiday>();

            CreateMap<Country, CountryCreateDto>();
            CreateMap<CountryCreateDto, Country>();

            CreateMap<Country, CountryUpdateDto>();
            CreateMap<CountryUpdateDto, Country>();

        }
    }
}
