using JohaRepository.Models;

namespace JohaRepository.Interfaces
{
    public interface ICachRepository<T>
        where T : class, IDomain<string>
    {
        void Set(T model);
        T Get(string id);
        void Update(T model);
        //ICachRepository<T> CreateWithConfig(CacheConfig model);
    }
}
