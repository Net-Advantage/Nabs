namespace Nabs.Persistence.Relational;

public class RelationalRepositoryOptions<TContext> : IRelationalRepositoryOptions<TContext>
    where TContext : DbContext
{
    public RelationalRepositoryOptions(
        IDbContextFactory<TContext> contextFactory,
        IMapper mapper)
    {
        ContextFactory = contextFactory;
        Mapper = mapper;
    }

    public IDbContextFactory<TContext> ContextFactory { get; init; }
    public IMapper Mapper { get; init; }
    
}