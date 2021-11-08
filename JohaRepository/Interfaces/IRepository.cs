using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JohaRepository.Interfaces
{
    public interface IRepository<T, TKey>
         where T : class, IDomain<TKey>
    {
        T Get(TKey id);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T model);
        void AddRange(IEnumerable<T> models);
        void Remove(T model);
        T Remove(TKey id);
        void RemoveRange(IEnumerable<T> models);
        void Update(T model);



    }
}
