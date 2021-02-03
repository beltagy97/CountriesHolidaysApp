using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data.Context
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
