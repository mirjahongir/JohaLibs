using JohaRepository.Enums;
using JohaRepository.Models;

namespace JohaRepository.Interfaces
{
    public interface IJohaLogger<TModel>
        where TModel : class
    {
        void AddLogger(LoggerModel model);
     
    }
}
