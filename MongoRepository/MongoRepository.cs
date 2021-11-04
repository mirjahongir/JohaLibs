﻿using JohaRepository;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MongoRepository
{
    public class MongoRepository<T> : IRepository<T, string>
       where T : class, IDomain<string>
    {
        public IMongoDatabase Database { get { return _data; } }
        IMongoDatabase _data;
        public IMongoCollection<T> Collection { get { return _db; } }
        public IMongoCollection<T> _db;
        public MongoRepository(IMongoDatabase data) : this(data, typeof(T).Name.ToLower())
        {

        }
        public MongoRepository(IMongoDatabase data, string collectionName)
        {
            _data = data;
            _db = data.GetCollection<T>(collectionName);
        }
        public virtual void Add(T model)
        {
            CheckAndAddId(model);
            _db.InsertOne(model);
        }
        private void CheckAndAddId(T model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                model.Id = ObjectId.GenerateNewId().ToString();
            }
        }

        public virtual void AddRange(IEnumerable<T> models)
        {
            foreach (var i in models)
            {
                CheckAndAddId(i);
            }
            _db.InsertMany(models);
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _db.AsQueryable<T>().Where(predicate);
        }

        public virtual T Get(string id)
        {
            return _db.Find(m => m.Id == id).FirstOrDefault();
        }

        public virtual IQueryable<T> GetAll()
        {
            return Find(m => true);
        }

        public virtual void Remove(T model)
        {
            _db.DeleteOne(m => m.Id == model.Id);
        }

        public virtual void RemoveRange(IEnumerable<T> models)
        {
            foreach (var i in models)
            {
                _db.DeleteOne(m => m.Id == i.Id);
            }

        }

        public virtual void Update(T model)
        {
            _db.FindOneAndReplace(m => m.Id == model.Id, model);
        }

        public virtual T Remove(string id)
        {
            var result = _db.Find(m => m.Id == id).FirstOrDefault();
            if (result == null) { return null; }
            _db.DeleteOne(m => m.Id == id);
            return result;

        }
    }
}
