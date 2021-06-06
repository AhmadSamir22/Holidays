using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.DTOs
{
    public class CountryApiDto
    {
        public string name { get; set; }
        public string alpha2code { get; set; }
        public string capital { get; set; }
    }
}
