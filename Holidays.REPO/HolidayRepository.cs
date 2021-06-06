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
    public class HolidayRepository : IHolidayRepository
    {
        private readonly HolidaysContext _context;

        public HolidayRepository(HolidaysContext context)
        {
            _context = context;
        }
        public async void Add(Holiday holiday)
        {
            await _context.AddAsync(holiday);
        }

        public async void Delete(int ID)
        {
            var holiday = await _context.Holidays.FirstOrDefaultAsync(h => h.ID == ID);
            holiday.Deleted = !holiday.Deleted;
        }

        public async Task<Holiday> GetHolidayById(int ID)
        {
            return await _context.Holidays.FirstOrDefaultAsync(h => h.ID == ID);
        }
        public async Task<Holiday> GetHolidayByName(string Name)
        {
            return await _context.Holidays.FirstOrDefaultAsync(h => h.Name == Name && h.Deleted == false);
        }

        public async Task<List<Holiday>> GetHolidays()
        {
            return await _context.Holidays.Where(h => h.Deleted == false).ToListAsync();
        }

        public async void Update(Holiday holiday)
        {
            var holidayDB = await _context.Holidays.FirstOrDefaultAsync(h => h.ID == holiday.ID);
            holidayDB = holiday;
            holidayDB.UpdatedAt = DateTime.Now;
        }

        public async Task<List<Holiday>> GetHolidaysByCountry(int CountryID)
        {
            return await _context.Holidays.Where(h => h.CountryID == CountryID && h.Deleted == false).ToListAsync();
        }
    }
}
