using Holidays.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holidays.REPO
{
    public class UnitOfWork
    {
        private readonly HolidaysContext _context;
        private CountryRepository _countryRepository;
        private HolidayRepository _holidayRepository;
        public UnitOfWork(HolidaysContext context)
        {
            _context = context;
        }

        public CountryRepository CountryRepo 
        {
            get {
                if (_countryRepository == null)
                    _countryRepository = new CountryRepository(_context);
                return _countryRepository;
            }
        }

        public HolidayRepository HolidayRepo
        {
            get
            {
                if (_holidayRepository == null)
                    _holidayRepository = new HolidayRepository(_context);
                return _holidayRepository;
            }
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
