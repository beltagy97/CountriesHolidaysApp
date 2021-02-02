using CountriesAndHolidaysApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesAndHolidaysApp.Services
{
     public interface ICountryHolidayServices
    {
        public string getCountryHolidays(string countryCode);

        public Task<object> sync();

        public bool deleteHoliday(string code , int id);

        public bool addHoliday(Holiday newHoliday);

        public bool modifyHoliday(int id, Holiday newHoliday);

        public string getCountries(int pageNumber);

    }
}
