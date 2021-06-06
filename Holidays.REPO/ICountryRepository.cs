using Holidays.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holidays.REPO
{
    public interface ICountryRepository
    {
        void Add(Country country);
        void Update(Country country);
        void Delete(int ID);
        Task<List<Country>> GetCountries();
        Task<List<Country>> GetAllCountries();

        Task<Country> GetCountryByCode(string Code);

        Task<Country> GetCountryById(int ID);

    }
}
