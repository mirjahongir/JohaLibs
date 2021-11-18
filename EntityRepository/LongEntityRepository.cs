using JohaRepository.Interfaces;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EntityRepository
{
    public class LongEntityRepository<T> : IRepository<T, long>
   where T : class, IDomain<long>
    {
        DbContext _db;
        DbSet<T> _set;
        public LongEntityRepository(IDataContext context)
        {
            _db = context.Context;
            _set = _db.Set<T>();


        }
        public DbSet<T> Set => _set;

        public virtual void Add(T model)
        {
            _set.Add(model);

            _db.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<T> models)
        {
            _set.AddRange(models);
            _db.SaveChanges();
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _set.Where(predicate);
        }

        public virtual T Get(long id)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return _set.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _set;
        }

        public virtual void Remove(T model)
        {
            _set.Remove(model);
            _db.SaveChanges();
        }

        public virtual T Remove(long id)
        {
            var model = Get(id);
            // Detached(model);
            Remove(model);
            return model;
        }

        public virtual void RemoveRange(IEnumerable<T> models)
        {
            _set.RemoveRange(models);

        }
        public virtual void Detached(T model)
        {
            _db.Entry(model).State = EntityState.Detached;
        }
        public virtual void Update(T model)
        {
            try
            {

                _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                _set.Update(model);
                _db.SaveChanges();
                Detached(model);
            }
            catch (Exception ext)
            {
                Console.WriteLine(ext.Message);
            }
            _db.SaveChanges();


        }
    }
}
