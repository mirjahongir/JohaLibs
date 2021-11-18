namespace JohaRepository.Interfaces
{
    public interface ICachRepository<T>
        where T : class, IDomain<string>
    {
        void Set(T model);
        void SetById(string id, T model);
        T Get(string id);

        void Update(T model);
        void UpdateByKey(string id, T model);
        void Remove(string id);
        //ICachRepository<T> CreateWithConfig(CacheConfig model);
    }
}
