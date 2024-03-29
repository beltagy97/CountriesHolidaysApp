﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Holiday
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String start_date { get; set; }
        [Required]
        public String end_date { get; set; }
        public int countryID { get; set; }
    }
}
