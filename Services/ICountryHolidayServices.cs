using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICountryHolidayServices
    {
        public string getCountryHolidays(string countryCode);

        public Task<object> sync();

        public string deleteHoliday(string code, int id);

        public string addHoliday(Holiday newHoliday);

        public string modifyHoliday(int id, Holiday newHoliday);

        public string getCountries(int pageNumber, int pageSize = 50);
    }
}
