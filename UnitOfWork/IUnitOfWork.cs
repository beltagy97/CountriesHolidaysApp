using System;
using System.Collections.Generic;
using System.Text;

namespace UnitOfWork
{
    public interface IUnitOfWork
    {
        public int SaveChanges();
        
    }
}
