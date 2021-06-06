using Holidays.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holidays.REPO
{
    public interface IHolidayRepository
    {
        void Add(Holiday holiday);
        void Update(Holiday holiday);
        void Delete(int ID);
        Task<List<Holiday>> GetHolidays();
        Task<Holiday> GetHolidayByName(string Name);
        Task<List<Holiday>> GetHolidaysByCountry(int CountryID);

        Task<Holiday> GetHolidayById(int ID);
    }
}
