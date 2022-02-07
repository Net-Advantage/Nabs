namespace Nabs.Persistence.Relational;

public class RelationalRepository<TDbContext> : IRelationalRepository<TDbContext>
    where TDbContext : DbContext
{
    private readonly IRelationalRepositoryOptions<TDbContext> _relationalRepositoryOptions;

    public RelationalRepository(IRelationalRepositoryOptions<TDbContext> relationalRepositoryOptions)
    {
        _relationalRepositoryOptions = relationalRepositoryOptions;

        _relationalRepositoryOptions.OnInitialise();
    }

    public TDbContext NewDbContext()
    {
        var result = _relationalRepositoryOptions.ContextFactory.CreateDbContext();
        return result;
    }

    public IQueryItem<TEntity> QueryItem<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>
    {
        var result = new QueryItem<TDbContext, TEntity>(_relationalRepositoryOptions);
        return result;
    }

    public IQuerySet<TEntity> QuerySet<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>
    {
        var result = new QuerySet<TDbContext, TEntity>(_relationalRepositoryOptions);
        return result;
    }

    public IItemCommand<TEntity> ItemCommand<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>
    {
        var result = new ItemCommand<TDbContext, TEntity>(_relationalRepositoryOptions);
        return result;
    }

    public ISetCommand<TEntity> SetCommand<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>
    {
        var result = new SetCommand<TDbContext, TEntity>(_relationalRepositoryOptions);
        return result;
    }
}

public class QueryItem<TDbContext, TEntity> : IQueryItem<TEntity>
    where TDbContext : DbContext
    where TEntity : class, IRelationalEntity<Guid>
{
    private readonly IRelationalRepositoryOptions<TDbContext> _relationalRepositoryOptions;
    private LambdaExpression _predicate;

    public QueryItem(IRelationalRepositoryOptions<TDbContext> relationalRepositoryOptions)
    {
        _relationalRepositoryOptions = relationalRepositoryOptions;
    }

    public IQueryItem<TEntity> WithId(Guid id)
    {
        return WithPredicate(_ => _.Id == id);
    }

    public IQueryItem<TEntity> WithPredicate(Expression<Func<TEntity, bool>> predicate)
    {
        _predicate = predicate;
        return this;
    }

    public async Task<TProjection> ExecuteAsync<TProjection>(CancellationToken cancellationToken = default)
        where TProjection : class, IDto
    {
        var result = await Execute<TProjection>(cancellationToken);
        return result;
    }

    public async Task<TEntity> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var result = await Execute<TEntity>(cancellationToken);
        return result;
    }

    private async Task<TProjection> Execute<TProjection>(CancellationToken cancellationToken = default)
        where TProjection : class
    {
        var context = await _relationalRepositoryOptions.ContextFactory.CreateDbContextAsync(cancellationToken);
        await context.Database.EnsureCreatedAsync(cancellationToken);
        var query = context.Set<TEntity>().AsNoTracking();
        if (_predicate != null)
        {
            var predicate = _predicate as Expression<Func<TEntity, bool>>;
            query = query.Where(predicate!);
        }

        if (typeof(TProjection) != typeof(TEntity))
        {
            var projection = query.ProjectTo<TProjection>(_relationalRepositoryOptions.Mapper.ConfigurationProvider);
            return await projection.FirstOrDefaultAsync(cancellationToken);
        }

        var result = await query.FirstOrDefaultAsync(cancellationToken);
        return result as TProjection;
    }
}

public class QuerySet<TDbContext, TEntity> : IQuerySet<TEntity>
    where TDbContext : DbContext
    where TEntity : class, IRelationalEntity<Guid>
{
    private readonly IRelationalRepositoryOptions<TDbContext> _relationalRepositoryOptions;
    private LambdaExpression _predicate;

    public QuerySet(IRelationalRepositoryOptions<TDbContext> relationalRepositoryOptions)
    {
        _relationalRepositoryOptions = relationalRepositoryOptions;
    }

    public IQuerySet<TEntity> WithPredicate(Expression<Func<TEntity, bool>> predicate)
    {
        _predicate = predicate;
        return this;
    }

    public async Task<IEnumerable<TEntity>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var context = await _relationalRepositoryOptions.ContextFactory.CreateDbContextAsync(cancellationToken);
        await context.Database.EnsureCreatedAsync(cancellationToken);
        var query = context.Set<TEntity>().AsNoTracking();
        if (_predicate != null)
        {
            var predicate = _predicate as Expression<Func<TEntity, bool>>;
            query = query.Where(predicate!);
        }
        var result = await query.ToListAsync(cancellationToken);
        return result;
    }
}

public class ItemCommand<TDbContext, TEntity> : IItemCommand<TEntity>
    where TDbContext : DbContext
    where TEntity : class, IRelationalEntity<Guid>
{
    private readonly IRelationalRepositoryOptions<TDbContext> _relationalRepositoryOptions;
    private TEntity _item;

    public ItemCommand(IRelationalRepositoryOptions<TDbContext> relationalRepositoryOptions)
    {
        _relationalRepositoryOptions = relationalRepositoryOptions;
    }

    public IItemCommand<TEntity> ForItem(TEntity item)
    {
        if (_item != null)
        {
            throw new InvalidOperationException("An item has already been queued. The 'ForItem' method can only be called once in an execution chain.");
        }
        _item = item;
        return this;
    }

    public async Task<TEntity> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var context = await _relationalRepositoryOptions.ContextFactory.CreateDbContextAsync(cancellationToken);
        await context.Database.EnsureCreatedAsync(cancellationToken);
        var set = context.Set<TEntity>();
        var existingItem = await set.FindAsync(_item.Id);
        if (existingItem == null)
        {
            set.Add(_item);
        }
        else
        {
            context.Entry(existingItem).CurrentValues.SetValues(_item);
        }
        var saveChangesResult = await context.SaveChangesAsync(cancellationToken);
        if (saveChangesResult != 1)
        {
            throw new Exception($"The item was not added or updated! {typeof(TEntity).Name}");
        }
        return _item;
    }
}

public class SetCommand<TDbContext, TEntity> : ISetCommand<TEntity>
    where TDbContext : DbContext
    where TEntity : class, IRelationalEntity<Guid>
{
    private readonly IRelationalRepositoryOptions<TDbContext> _relationalRepositoryOptions;
    private IEnumerable<TEntity> _items;

    public SetCommand(IRelationalRepositoryOptions<TDbContext> relationalRepositoryOptions)
    {
        _relationalRepositoryOptions = relationalRepositoryOptions;
    }

    public ISetCommand<TEntity> ForItems(IEnumerable<TEntity> items)
    {
        if (_items != null)
        {
            throw new InvalidOperationException("An item has already been queued. The 'ForItem' method can only be called once in an execution chain.");
        }
        _items = items;
        return this;
    }

    public async Task<IEnumerable<TEntity>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var context = await _relationalRepositoryOptions.ContextFactory.CreateDbContextAsync(cancellationToken);
        await context.Database.EnsureCreatedAsync(cancellationToken);
        var set = context.Set<TEntity>();
        var ids = _items.Select(_ => _.Id).ToArray();
        var existingItems = await set
            .Where(_ => ids.Contains(_.Id))
            .ToListAsync(cancellationToken);
        var existingIds = existingItems.Select(_ => _.Id).ToArray();
        var newItems = _items.Where(_ => !existingIds.Contains(_.Id)).ToArray();
        
        foreach (var item in _items)
        {
            var existingItem = existingItems.Find(_ => _.Id == item.Id);
            if (existingItem == null)
            {
                set.AddRange(newItems);
            }
            else
            {
                context.Entry(existingItem).CurrentValues.SetValues(item);
            }
        }
        
        _ = await context.SaveChangesAsync(cancellationToken);
        
        return _items;
    }
}