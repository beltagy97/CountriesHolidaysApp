using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesAndHolidaysApp.Models
{
    public class CountryHolidayResultSet
    {
        public String countryName { get; set; }
        public String code { get; set; }
        public String holidayName { get; set; }
        public String start_date { get; set; }
        public String end_date { get; set; }
    }
}
