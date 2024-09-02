using System;
using System.Collections.Generic;
using System.Text;

namespace Core.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Func<T, bool> predicate = null);
        T Get(Func<T, bool> predicate);
        //void Add(T entity);
        //void Attach(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
