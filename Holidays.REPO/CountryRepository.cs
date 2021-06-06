using Holidays.DAL;
using Holidays.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holidays.REPO
{
    public class CountryRepository : ICountryRepository
    {
        private readonly HolidaysContext _context;

        public CountryRepository(HolidaysContext context)
        {
            _context = context;
        }
        public async void Add(Country country)
        {
            country.CreatedAt = DateTime.Now;
            country.UpdatedAt = DateTime.Now;
            await _context.Countries.AddAsync(country);
        }

        public async void Delete(int ID)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.ID == c.ID);
            country.Deleted = !country.Deleted;
        }

        public async Task<List<Country>> GetCountries()
        {
            return await _context.Countries.Where(c => c.Deleted == false).ToListAsync();
        }

        public async Task<Country> GetCountryByCode(string Code)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Code == Code && c.Deleted == false);
        }
        public async Task<Country> GetCountryById(int ID)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.ID == ID);
        }

        public async void Update(Country country)
        {
            var countryDB = await _context.Countries.FirstOrDefaultAsync(c => c.ID == country.ID);
        }
    }
}
