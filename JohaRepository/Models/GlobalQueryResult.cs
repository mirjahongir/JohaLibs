using System.Collections.Generic;

namespace JohaRepository.Models
{
    public class GlobalQueryResult<T>
    {
        public int Count { get; set; }
        public List<T> Data { get; set; }


    }
}