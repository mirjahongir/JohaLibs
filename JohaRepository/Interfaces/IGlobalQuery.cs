using System;

namespace JohaRepository.Interfaces
{
    public interface IGlobalQuery<TKey>
    {
        TKey Id { get; set; }
        string Name { get; set; }
        int PageNumber { get; set; }

        DateTime DateTime { get; set; }
        int PageSize { get; set; }
    }

}
