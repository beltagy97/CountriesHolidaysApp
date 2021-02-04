using Data;
using Data.Context;
using System;

namespace UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CountriesAndHolidaysContext context;
        public UnitOfWork(IDBFactory factory)
        {
            this.context = factory.getDB();
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
