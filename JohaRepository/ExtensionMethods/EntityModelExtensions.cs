using JohaRepository.Interfaces;
using JohaRepository.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace JohaRepository.ExtensionMethods
{
    public static class EntityModelExtensions
    {
        public static GlobalQueryResult<T> QueryString<T, K>(this IQueryable<T> data, K query)
            where T : class, IEntityModel, IDomain<string>
            where K : class, IGlobalQuery<string>
        {
            if (!string.IsNullOrEmpty(query.Id))
            {
                data = data.Where(m => m.Id == query.Id);
            }

            if (!string.IsNullOrEmpty(query.Name))
            {
                data = data.Where(m => m.Name.Contains(query.Name));
            }
            if (query.DateTime != default(DateTime))
            {
                data = data.Where(m => m.LastUpdateDate >= query.DateTime);
            }

            var result = new GlobalQueryResult<T>();
            result.Count = data.Count();

            result.Data = data.OrderByDescending(m => m.Id).Skip(query.PageSize * query.PageNumber).Take(query.PageSize).ToList();
            return result;
        }
        public static GlobalQueryResult<T> QueryLong<T, K>(this IQueryable<T> data, K query)
            where T : class, IEntityModel, IDomain<long>
            where K : class, IGlobalQuery<long>
        {
            if (query.Id != 0)
            {
                data = data.Where(m => m.Id == query.Id);
            }
            if (!string.IsNullOrEmpty(query.Name))
            {
                data = data.Where(m => m.Name.Contains(query.Name));
            }
            if (query.DateTime != default(DateTime))
            {
                data = data.Where(m => m.LastUpdateDate >= query.DateTime);
            }
            var result = new Models.GlobalQueryResult<T>();
            result.Count = data.Count();
            result.Data = data.OrderByDescending(m => m.Id).Skip(query.PageSize * query.PageNumber).Take(query.PageSize).ToList();
            return result;

        }
        public static GlobalQueryResult<T> QueryInt<T, K>(this IQueryable<T> data, K query)
            where T : class, IEntityModel, IDomain<int>
            where K : class, IGlobalQuery<int>
        {
            if (query.Id != 0)
            {
                data = data.Where(m => m.Id == query.Id);
            }
            if (!string.IsNullOrEmpty(query.Name))
            {
                data = data.Where(m => m.Name.Contains(query.Name));
            }
            if (query.DateTime != default(DateTime))
            {
                data = data.Where(m => m.LastUpdateDate >= query.DateTime);
            }
            var result = new Models.GlobalQueryResult<T>();
            result.Count = data.Count();
            result.Data = data.OrderByDescending(m => m.Id).Skip(query.PageSize * query.PageNumber).Take(query.PageSize).ToList();
            return result;

        }
        public static void Add(object model)
        {
            if (model is IEntityModel entity)
            {
                entity.CreateDate = DateTime.Now;
                entity.LastUpdateDate = DateTime.Now;
                if (string.IsNullOrEmpty(entity.Name))
                {
                    throw new JohaRepository.Exceptions.RepoException() { Code = -20, Status = 400, ErrorModels = new List<JohaRepository.Models.ErrorModels.ErrorModal>() { new JohaRepository.Models.ErrorModels.ErrorModal() { HttpStatus = 400, Message = "Name Is null " } } };
                }
                CheckName(entity);
            }
        }
        internal static void CheckName(IEntityModel entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new JohaRepository.Exceptions.RepoException() { Code = -20, Status = 400, ErrorModels = new List<JohaRepository.Models.ErrorModels.ErrorModal>() { new JohaRepository.Models.ErrorModels.ErrorModal() { HttpStatus = 400, Message = "Name Is null " } } };
            }

        }
        public static void Update(object model)
        {
            if (model is IEntityModel entity)
            {
                entity.LastUpdateDate = DateTime.Now;
                CheckName(entity);
            }
        }
    }
}
