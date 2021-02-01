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
        Task<String> GetDataFromAPI(String url);

        public IList<Country> CountryJSONParser(string response);

        public IList<Holiday> HolidayJSONParser(string response);

        public Task<object> sync();

        public bool deleteHoliday(string code , int id);

        public bool AddHoliday(Holiday newHoliday);

    }
}
