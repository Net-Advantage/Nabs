namespace Nabs.Persistence.Relational;

public interface IRelationalRepository<out TDbContext>
    where TDbContext : DbContext
{
    TDbContext NewDbContext();

    IQueryItem<TEntity> QueryItem<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>;

    IItemCommand<TEntity> ItemCommand<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>;
}

public interface IQueryItem<TEntity>
    where TEntity : class, IRelationalEntity<Guid>
{
    IQueryItem<TEntity> WithId(Guid id);

    IQueryItem<TEntity> WithPredicate(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IQuerySet<TEntity>
    where TEntity : class, IRelationalEntity<Guid>
{
    IQuerySet<TEntity> WithPredicate(Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<TEntity>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IItemCommand<TEntity>
    where TEntity : class, IRelationalEntity<Guid>
{
    IItemCommand<TEntity> ForItem(TEntity item);

    Task<TEntity> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface ISetCommand<TEntity>
    where TEntity : class, IRelationalEntity<Guid>
{
    IItemCommand<TEntity> ForItems(IEnumerable<TEntity> items);

    Task<IEnumerable<TEntity>> ExecuteAsync(CancellationToken cancellationToken = default);
}