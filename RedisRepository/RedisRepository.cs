using JohaRepository.Interfaces;

using Newtonsoft.Json;

using StackExchange.Redis;

namespace RedisRepositorys
{
    public class RedisRepository<T> : ICachRepository<T>
        where T : class, IDomain<string>
    {
        IDatabase _db;
        public RedisRepository(ConnectionMultiplexer client, int db)
        {
            _db = client.GetDatabase(db);
        }

        //public ICachRepository<T> CreateWithConfig(CacheConfig model)
        //{

        //}

        public T Get(string id)
        {
            var model = _db.StringGet(id);
            if (model.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(model.ToString());
            }
            return null;
        }

        public void Remove(string id)
        {

        }

        public void Set(T model)
        {
            SetById(model.Id, model);
        }

        public void SetById(string id, T model)
        {
            var data = JsonConvert.SerializeObject(model);
            _db.StringSet(id, data);
        }

        public void Update(T model)
        {
            UpdateByKey(model.Id, model);

        }

        public void UpdateByKey(string id, T model)
        {
            _db.StringSet(id, JsonConvert.SerializeObject(model));
        }
    }
}
