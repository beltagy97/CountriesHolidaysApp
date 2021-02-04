using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Data
{
    public class DBFactory : IDBFactory
    {
        private CountriesAndHolidaysContext context;
        private string mySqlConnectionStr;

        public DBFactory(ConnString conn)
        {
            this.mySqlConnectionStr = conn.ConnectionString;
        }


        public CountriesAndHolidaysContext createDB()
        {
           var options = new DbContextOptionsBuilder<CountriesAndHolidaysContext>()
                  .UseMySql(this.mySqlConnectionStr).Options;
            context = new CountriesAndHolidaysContext(options);
            return context;
        }

        public CountriesAndHolidaysContext getDB()
        {
            
            return context == null ? createDB():context;
        }
    }
}
