using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Implementations
{
    public class CountryRepository
    {

        private readonly CountriesAndHolidaysContext context;
        internal DbSet<Country> dbSet;


        public CountryRepository(CountriesAndHolidaysContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Country>();
        }


        public Country getCountryByID(int id)
        {
            return context.Countries.Where(country => country.CountryID == id).SingleOrDefault();
        }

        public void Add(Country country)
        {
            this.dbSet.Add(country);
            context.SaveChanges();
        }


        public object getCountriesList(int pageNumber, int pageSize)
        {
            int skippedPages = (pageNumber - 1) * pageSize;
            return context.Countries.Skip(skippedPages).Take(pageSize).Select(country => new { Name = country.name, Code = country.code }).ToList();
        }

        public IList<CountryHolidayResultSet> getCountryHolidays(string code)
        {

            IList<CountryHolidayResultSet> Holidays = this.dbSet.Join(context.Holidays,
                country => country.CountryID,
                holiday => holiday.countryID,
                (country, holiday) => new CountryHolidayResultSet
                {
                    countryName = country.name,
                    code = country.code,
                    holidayName = holiday.Name,
                    start_date = holiday.start_date,
                    end_date = holiday.end_date
                }).Where(o => o.code == code).ToList();

            return Holidays;
        }

        public bool Exists(int id)
        {
            return this.dbSet.Any(country => country.CountryID == id);
        }


        public Country FindbyCode(string code)
        {
            return this.context.Countries.Where(country => country.code == code).FirstOrDefault();
        }

        
        
    }
}
