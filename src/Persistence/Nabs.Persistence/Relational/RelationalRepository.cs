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

    public IItemCommand<TEntity> ItemCommand<TEntity>()
        where TEntity : class, IRelationalEntity<Guid>
    {
        var result = new ItemCommand<TDbContext, TEntity>(_relationalRepositoryOptions);
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

    public async Task<TEntity> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var context = await _relationalRepositoryOptions.ContextFactory.CreateDbContextAsync(cancellationToken);
        await context.Database.EnsureCreatedAsync(cancellationToken);
        var query = context.Set<TEntity>().AsNoTracking();
        if (_predicate != null)
        {
            var predicate = _predicate as Expression<Func<TEntity, bool>>;
            query = query.Where(predicate!);
        }
        var result = await query.FirstOrDefaultAsync(cancellationToken);
        return result;
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