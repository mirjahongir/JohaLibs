using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JohaRepository
{
    public interface IDomain<TKey>
    {
        TKey Id { get; set; }
    }
    public interface ICachRepository<T>
        where T : class, IDomain<string>
    {
        void Set(T model);
        T Get(string id);
        void Update(T model);
    }
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
