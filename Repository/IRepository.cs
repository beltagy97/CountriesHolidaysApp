using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, object>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Exists(int id);
    }
}
