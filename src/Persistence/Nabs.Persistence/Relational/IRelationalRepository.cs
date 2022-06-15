namespace Nabs.Persistence.Relational;

public interface IRelationalRepository<out TDbContext>
	where TDbContext : DbContext
{
	TDbContext NewDbContext();

	ISelectItem<TEntity> SelectItem<TEntity>()
		where TEntity : class, IRelationalEntity<Guid>;

	ISelectSet<TEntity> SelectSet<TEntity>()
		where TEntity : class, IRelationalEntity<Guid>;

	IUpsertItem<TEntity> UpsertItem<TEntity>()
		where TEntity : class, IRelationalEntity<Guid>;

	IUpsertSet<TEntity> UpsertSet<TEntity>()
		where TEntity : class, IRelationalEntity<Guid>;

	IDeleteItem<TEntity> DeleteItem<TEntity>()
		where TEntity : class, IRelationalEntity<Guid>;

	IDeleteSet<TEntity> DeleteSet<TEntity>()
		where TEntity : class, IRelationalEntity<Guid>;
}

public interface ISelectItem<TEntity>
	where TEntity : class, IRelationalEntity<Guid>
{
	ISelectItem<TEntity> WithId(Guid id);

	ISelectItem<TEntity> WithPredicate(Expression<Func<TEntity, bool>> predicate);

	Task<TProjection> ExecuteAsync<TProjection>(CancellationToken cancellationToken = default)
		where TProjection : class, IDto;

	Task<TEntity> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface ISelectSet<TEntity>
	where TEntity : class, IRelationalEntity<Guid>
{
	ISelectSet<TEntity> WithPredicate(Expression<Func<TEntity, bool>> predicate);

	Task<IEnumerable<TEntity>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IUpsertItem<TEntity>
	where TEntity : class, IRelationalEntity<Guid>
{
	IUpsertItem<TEntity> ForItem(TEntity item);

	Task<TEntity> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IUpsertSet<TEntity>
	where TEntity : class, IRelationalEntity<Guid>
{
	IUpsertSet<TEntity> ForItems(IEnumerable<TEntity> items);

	Task<IEnumerable<TEntity>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IDeleteItem<TEntity>
	where TEntity : class, IRelationalEntity<Guid>
{
	IDeleteItem<TEntity> ForItem(TEntity item);

	Task<TEntity> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IDeleteSet<TEntity>
	where TEntity : class, IRelationalEntity<Guid>
{
	IDeleteSet<TEntity> ForItems(IEnumerable<TEntity> items);

	Task<IEnumerable<TEntity>> ExecuteAsync(CancellationToken cancellationToken = default);
}