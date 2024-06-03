namespace Nabs.Persistence;

public sealed class EntityRepository<TEntity>(
    DbContext dbContext) : RepositoryBase<TEntity>(dbContext)
    where TEntity : class
{
}
