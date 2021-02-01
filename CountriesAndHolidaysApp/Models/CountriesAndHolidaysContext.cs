using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesAndHolidaysApp.Models
{
    public class CountriesAndHolidaysContext : DbContext
    {

        public DbSet<Country> Countries { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        public CountriesAndHolidaysContext(DbContextOptions<CountriesAndHolidaysContext> options) : base(options)
        {
        }

    }
}
