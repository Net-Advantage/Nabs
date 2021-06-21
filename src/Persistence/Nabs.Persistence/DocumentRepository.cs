using System.Threading.Tasks;

namespace Nabs.Persistence
{
    public class DocumentRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public Task<TEntity> GetItem()
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> GetItems()
        {
            throw new System.NotImplementedException();
        }
    }
}