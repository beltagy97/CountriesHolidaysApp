using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICountryHolidayServices
    {
        public string getCountryHolidays(string countryCode);

        public Task<ResponseMessage> sync();

        public ResponseMessage deleteHoliday(string code, int id);

        public ResponseMessage addHoliday(Holiday newHoliday);

        public ResponseMessage modifyHoliday(int id, Holiday newHoliday);

        public string getCountries(int pageNumber, int pageSize = 50);
    }
}
