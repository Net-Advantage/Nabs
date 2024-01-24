
namespace Nabs.Persistence;

public sealed class EntityRepository<TEntity> : RepositoryBase<TEntity>
	where TEntity : class
{
	public EntityRepository(
		DbContext dbContext)
		: base(dbContext)
	{
	}
}
