using Data.Context;
using Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Implementations
{
    public class CountryRepository :ICountryRepository
    {

        private readonly CountriesAndHolidaysContext context;
        private readonly DbSet<Country> dbSet;


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
            
        }


        public List<CountryList> getCountriesList(int pageNumber, int pageSize)
        {
            int skippedPages = (pageNumber - 1) * pageSize;
            return context.Countries.Skip(skippedPages).Take(pageSize).Select(country => new CountryList{ Name = country.name, code = country.code }).ToList();
        }

        public List<CountryHolidayResultSet> getCountryHolidays(string code)
        {

            List<CountryHolidayResultSet> Holidays = this.dbSet.Join(context.Holidays,
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
