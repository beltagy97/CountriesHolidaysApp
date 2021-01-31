﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesAndHolidaysApp.Models
{
    public class Countries
    {
        [Key]
        public int CountryID { get; set; }
        [Required]
        public String Code { get; set; }
        [Required]
        public String Name { get; set; }

        public ICollection<Holidays> Holidays { get; set; }

    }
}
