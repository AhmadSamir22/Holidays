using Holidays.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Holidays.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        public Uri uri = new Uri("https://ajayakv-rest-countries-v1.p.rapidapi.com/rest/v1/all");
        string rapidKey = "dbae67d014msh28b408e51d56eb8p16128fjsn94ed807f1180";
        public async Task<IActionResult> Index()
        {
            client.DefaultRequestHeaders.Add("x-rapidapi-key", rapidKey);
            var res = await client.GetAsync(uri);
            var content = res.Content;
            var result = await content.ReadAsStringAsync();
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            var countries = json_serializer.DeserializeObject(result);
            var c = (List<CountryApiDto>)countries;
            return Ok(countries);
        }
    }
}
