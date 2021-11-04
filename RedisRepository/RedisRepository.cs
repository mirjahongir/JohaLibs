using JohaRepository;

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
        public T Get(string id)
        {
            var model = _db.StringGet(id);
            if (model.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(model.ToString());
            }
            return null;
        }

        public void Set(T model)
        {
            var data = JsonConvert.SerializeObject(model);
            _db.StringSet(model.Id, data);
        }

        public void Update(T model)
        {
            _db.StringSet(model.Id, JsonConvert.SerializeObject(model));
        }
    }
}
