using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IHolidayRepository
    {
        public void CreateHoliday(Holiday holiday);
        public void UpdateHoliday(Holiday holiday, Holiday newHoliday);
        public Holiday Find(int id);
        public void Delete(Holiday holiday);
        public bool Exists(int id);
    }
}
