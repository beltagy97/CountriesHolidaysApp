using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IDBFactory
    {
        public CountriesAndHolidaysContext getDB();

        public CountriesAndHolidaysContext createDB();

    }
}
