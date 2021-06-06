using AutoMapper;
using Holidays.API.DTOs;
using Holidays.REPO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Holidays.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly UnitOfWork _workUnit;
        private readonly IMapper _mapper;
        public Uri uri = new Uri("https://ajayakv-rest-countries-v1.p.rapidapi.com/rest/v1/all");
        string rapidKey = "dbae67d014msh28b408e51d56eb8p16128fjsn94ed807f1180";

        public SyncController(UnitOfWork workUnit, IMapper mapper)
        {
            _workUnit = workUnit;
            _mapper = mapper;
        }
        [HttpGet("SyncCountries")]
        public async Task<IActionResult> SyncCountries()
        {
            var apiData = await GetCountriesFromAPI();
            var countries = await _workUnit.CountryRepo.GetAllCountries();
            var localData = _mapper.Map<CountryApiDto>(countries);
            return Ok();
        }
        
        public async Task<List<CountryApiDto>> GetCountriesFromAPI()
        {
            client.DefaultRequestHeaders.Add("x-rapidapi-key", rapidKey);
            var res = await client.GetAsync(uri);
            var content = res.Content;
            var result = await content.ReadAsStringAsync();


            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(result));
            ms.Position = 0;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<CountryApiDto>));
            List<CountryApiDto> countries = (List<CountryApiDto>)ser.ReadObject(ms);

            return countries;
        }


    }
}
