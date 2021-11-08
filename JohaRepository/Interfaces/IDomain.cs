namespace JohaRepository.Interfaces
{
    public interface IDomain<TKey>
    {
        TKey Id { get; set; }
    }
}
