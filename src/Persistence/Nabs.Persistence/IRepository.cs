using System.Threading.Tasks;

namespace Nabs.Persistence
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetItem();

        Task<TEntity> GetItems();
    }
}