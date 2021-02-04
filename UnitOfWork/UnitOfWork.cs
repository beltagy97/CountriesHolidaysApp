using Data.Context;
using System;

namespace UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CountriesAndHolidaysContext context;
        public UnitOfWork(CountriesAndHolidaysContext context)
        {
            this.context = context;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
