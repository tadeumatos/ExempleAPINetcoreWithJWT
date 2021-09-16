using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bookstore.Data.Respository
{
    public interface IRepository<T>
    {
       IQueryable<T> GetAll();
       IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
       void Create(T entity);
       void Update(T entity);
       void Delete (T entity);
    }
}
