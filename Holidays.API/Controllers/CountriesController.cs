using AutoMapper;
using Holidays.API.DTOs;
using Holidays.DAL.Models;
using Holidays.REPO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly UnitOfWork _workUnit;
        private readonly IMapper _mapper;

        public CountriesController(UnitOfWork workUnit, IMapper mapper)
        {
            _workUnit = workUnit;
            _mapper = mapper;
        }

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _workUnit.CountryRepo.GetCountries();
            var response = new
            {
                Countries = countries
            };

            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCountry(CountryCreateDto countryDto)
        {
            var countryExist = _workUnit.CountryRepo.GetCountryByCode(countryDto.Code);
            if (countryExist != null)
                return BadRequest(new { Message = "Country with given Code already Exists" });
            var country = _mapper.Map<Country>(countryDto);
            _workUnit.CountryRepo.Add(country);
            await _workUnit.Save();
            return Ok(new { Message = "Country Added Successifully" });
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCountry(CountryUpdateDto countryDto)
        {
            var countryExist = _workUnit.CountryRepo.GetCountryById(countryDto.ID);
            if (countryExist == null)
                return BadRequest(new { Message = "Country with given ID doesn't Exist" });
            var country = _mapper.Map<Country>(countryDto);
            _workUnit.CountryRepo.Update(country);
            await _workUnit.Save();
            return Ok(new { Message = "Country Updated Successifully" });
        }

        [HttpPost("Toggle")]
        //if Deleted reverese and vice versa
        public async Task<IActionResult> ToggleCountry(int id)
        {
            var country = _workUnit.CountryRepo.GetCountryById(id);
            if (country == null)
                return BadRequest(new { Message = "Country with given ID doesn't Exist" });
            _workUnit.CountryRepo.Delete(id);
            await _workUnit.Save();
            return Ok(new { Message = "Toggled Successifully" });
        }
    }
}
