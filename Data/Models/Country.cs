using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class Country
    {
        [Key]
        public int CountryID { get; set; }
        public String name { get; set; }
        public String code { get; set; }
        public ICollection<Holiday> Holidays { get; set; }
    }
}
