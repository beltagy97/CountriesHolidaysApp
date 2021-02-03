using Data.Context;
using Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Implementations
{
    public class HolidayRepository
    {

        private readonly CountriesAndHolidaysContext context;
        internal DbSet<Holiday> dbSet;

        public HolidayRepository(CountriesAndHolidaysContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Holiday>();
        }


        public void CreateHoliday(Holiday holiday)
        {
            this.dbSet.Add(holiday);
            this.context.SaveChanges();
        }

        public void UpdateHoliday(Holiday holiday, Holiday newHoliday)
        {
            holiday.Name = newHoliday.Name;
            holiday.start_date = newHoliday.start_date;
            holiday.end_date = newHoliday.end_date;
            holiday.countryID = newHoliday.countryID;
            this.context.SaveChanges();
        }

        public Holiday Find(int id)
        {
            return dbSet.Where(holiday => holiday.ID == id).FirstOrDefault();
        }

        public void Delete(Holiday holiday)
        {
            this.dbSet.Remove(holiday);
            this.context.SaveChanges();
        }


        public bool Exists(int id)
        {
            return this.dbSet.Any(holiday => holiday.ID == id);
        }
    }
}
