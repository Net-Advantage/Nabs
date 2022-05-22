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

    public ISelectItem<TEntity> SelectItem<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>
    {
        var result = new SelectItem<TDbContext, TEntity>(_relationalRepositoryOptions);
        return result;
    }

    public ISelectSet<TEntity> SelectSet<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>
    {
        var result = new SelectSet<TDbContext, TEntity>(_relationalRepositoryOptions);
        return result;
    }

    public IUpsertItem<TEntity> UpsertItem<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>
    {
        var result = new UpsertItem<TDbContext, TEntity>(_relationalRepositoryOptions);
        return result;
    }

    public IUpsertSet<TEntity> UpsertSet<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>
    {
        var result = new UpsertSet<TDbContext, TEntity>(_relationalRepositoryOptions);
        return result;
    }

    public IDeleteItem<TEntity> DeleteItem<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>
    {
        var result = new DeleteItem<TDbContext, TEntity>(_relationalRepositoryOptions);
        return result;
    }

    public IDeleteSet<TEntity> DeleteSet<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>
    {
        var result = new DeleteSet<TDbContext, TEntity>(_relationalRepositoryOptions);
        return result;
    }
}

public class SelectItem<TDbContext, TEntity> : ISelectItem<TEntity>
    where TDbContext : DbContext
    where TEntity : class, IRelationalEntity<Guid>
{
    private readonly IRelationalRepositoryOptions<TDbContext> _relationalRepositoryOptions;
    private LambdaExpression _predicate;

    public SelectItem(IRelationalRepositoryOptions<TDbContext> relationalRepositoryOptions)
    {
        _relationalRepositoryOptions = relationalRepositoryOptions;
    }

    public ISelectItem<TEntity> WithId(Guid id)
    {
        return WithPredicate(_ => _.Id == id);
    }

    public ISelectItem<TEntity> WithPredicate(Expression<Func<TEntity, bool>> predicate)
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

public class SelectSet<TDbContext, TEntity> : ISelectSet<TEntity>
    where TDbContext : DbContext
    where TEntity : class, IRelationalEntity<Guid>
{
    private readonly IRelationalRepositoryOptions<TDbContext> _relationalRepositoryOptions;
    private LambdaExpression _predicate;

    public SelectSet(IRelationalRepositoryOptions<TDbContext> relationalRepositoryOptions)
    {
        _relationalRepositoryOptions = relationalRepositoryOptions;
    }

    public ISelectSet<TEntity> WithPredicate(Expression<Func<TEntity, bool>> predicate)
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

public class UpsertItem<TDbContext, TEntity> : IUpsertItem<TEntity>
    where TDbContext : DbContext
    where TEntity : class, IRelationalEntity<Guid>
{
    private readonly IRelationalRepositoryOptions<TDbContext> _relationalRepositoryOptions;
    private TEntity _item;

    public UpsertItem(IRelationalRepositoryOptions<TDbContext> relationalRepositoryOptions)
    {
        _relationalRepositoryOptions = relationalRepositoryOptions;
    }

    public IUpsertItem<TEntity> ForItem(TEntity item)
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

public class UpsertSet<TDbContext, TEntity> : IUpsertSet<TEntity>
    where TDbContext : DbContext
    where TEntity : class, IRelationalEntity<Guid>
{
    private readonly IRelationalRepositoryOptions<TDbContext> _relationalRepositoryOptions;
    private IEnumerable<TEntity> _items;

    public UpsertSet(IRelationalRepositoryOptions<TDbContext> relationalRepositoryOptions)
    {
        _relationalRepositoryOptions = relationalRepositoryOptions;
    }

    public IUpsertSet<TEntity> ForItems(IEnumerable<TEntity> items)
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

public class DeleteItem<TDbContext, TEntity> : IDeleteItem<TEntity>
    where TDbContext : DbContext
    where TEntity : class, IRelationalEntity<Guid>
{
    private readonly IRelationalRepositoryOptions<TDbContext> _relationalRepositoryOptions;
    private TEntity _item;

    public DeleteItem(IRelationalRepositoryOptions<TDbContext> relationalRepositoryOptions)
    {
        _relationalRepositoryOptions = relationalRepositoryOptions;
    }

    public IDeleteItem<TEntity> ForItem(TEntity item)
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
        if (existingItem != null)
        {
            set.Remove(existingItem);
        }
        
        var saveChangesResult = await context.SaveChangesAsync(cancellationToken);
        if (saveChangesResult != 1)
        {
            throw new Exception($"The item was not deleted! {typeof(TEntity).Name}");
        }
        return _item;
    }
}

public class DeleteSet<TDbContext, TEntity> : IDeleteSet<TEntity>
    where TDbContext : DbContext
    where TEntity : class, IRelationalEntity<Guid>
{
    private readonly IRelationalRepositoryOptions<TDbContext> _relationalRepositoryOptions;
    private IEnumerable<TEntity> _items;

    public DeleteSet(IRelationalRepositoryOptions<TDbContext> relationalRepositoryOptions)
    {
        _relationalRepositoryOptions = relationalRepositoryOptions;
    }

    public IDeleteSet<TEntity> ForItems(IEnumerable<TEntity> items)
    {
        if (_items != null)
        {
            throw new InvalidOperationException("Item have already been queued. The 'ForItems' method can only be called once in an execution chain.");
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

        if (existingItems.Any())
        {
            set.RemoveRange(existingItems);
        }

        _ = await context.SaveChangesAsync(cancellationToken);

        return _items;
    }
}

