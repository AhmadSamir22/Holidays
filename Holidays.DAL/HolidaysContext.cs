using Holidays.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Holidays.DAL
{
    public class HolidaysContext : DbContext
    {
        public HolidaysContext(DbContextOptions<HolidaysContext>options) : base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(@"
                    server=localhost;
                    database=HolidayDB;
                    user=root;
                    password=;
                    port=3305;
                ");
        }


    }
}
