using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesAndHolidaysApp.Models
{
    public class Holidays
    {
        public int Id { get; set; }

        public String Name { get; set; }
        public String start_date { get; set; }

        public String end_date { get; set; }

        public Countries Country { get; set; }
    }
}
