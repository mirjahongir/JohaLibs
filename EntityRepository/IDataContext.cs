
using Microsoft.EntityFrameworkCore;

namespace EntityRepository
{
    public interface IDataContext
    {
        DbContext Context { get; }
    }
}
