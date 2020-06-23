using System;
using System.Collections.Generic;

namespace AskAppMVC6.DAL.Repositories
{
    public interface IRepository<T> where T: class
    {
        bool Delete(T entity);
        T Get(int id);
        ICollection<T> GetAll();
        ICollection<T> GetByPredicate(Func<T, bool> predicate);
        T Insert(T entity);
        T Update(T entity);
    }
}