using Data.Context;
using Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;

namespace Repository.Implementations
{
    public class HolidayRepository: IHolidayRepository
    {

        private readonly CountriesAndHolidaysContext context;
        internal DbSet<Holiday> dbSet;

        public HolidayRepository(IDBFactory factory)
        {
            this.context = factory.getDB();
            this.dbSet = context.Set<Holiday>();
        }


        public void CreateHoliday(Holiday holiday)
        {
            this.dbSet.Add(holiday);
            
        }

        public void UpdateHoliday(Holiday holiday, Holiday newHoliday)
        {
            holiday.Name = newHoliday.Name;
            holiday.start_date = newHoliday.start_date;
            holiday.end_date = newHoliday.end_date;
            holiday.countryID = newHoliday.countryID;
            
        }

        public Holiday Find(int id)
        {
            return dbSet.Where(holiday => holiday.ID == id).FirstOrDefault();
        }

        public void Delete(Holiday holiday)
        {
            this.dbSet.Remove(holiday);
            
        }


        public bool Exists(int id)
        {
            return this.dbSet.Any(holiday => holiday.ID == id);
        }
    }
}
