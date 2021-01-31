using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesAndHolidaysApp.Models
{
    public class CountriesAndHolidaysContext : DbContext
    {

        public DbSet<Countries> Countries { get; set; }
        public DbSet<Holidays> Holidays { get; set; }

        public CountriesAndHolidaysContext(DbContextOptions<CountriesAndHolidaysContext> options) : base(options)
        {
        }

    }
}
