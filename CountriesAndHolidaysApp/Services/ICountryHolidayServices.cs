using CountriesAndHolidaysApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesAndHolidaysApp.Services
{
    interface ICountryHolidayServices
    {
        Task<String> GetDataFromAPI(String url);

        public IList<Countries> CountryJSONParser(string response);

        public IList<Holidays> HolidayJSONParser(string response);

        public Task<object> sync(CountriesAndHolidaysContext context);

    }
}
