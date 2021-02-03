using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface ICountryRepository
    {
        public Country getCountryByID(int id);
        public void Add(Country country);
        public List<Country> getCountriesList(int pageNumber, int pageSize);
        public List<CountryHolidayResultSet> getCountryHolidays(string code);
        public bool Exists(int id);
        public Country FindbyCode(string code);

    }
}
