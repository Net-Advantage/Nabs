namespace Nabs.Persistence.Relational;

public class RelationalRepositoryOptions<TContext> : IRelationalRepositoryOptions<TContext>
    where TContext : DbContext
{
    public RelationalRepositoryOptions(IDbContextFactory<TContext> contextFactory)
    {
        ContextFactory = contextFactory;
    }

    public IDbContextFactory<TContext> ContextFactory { get; init; }
    
}