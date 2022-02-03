namespace Nabs.Persistence.Relational;

public interface IRelationalRepositoryOptions<TContext>
    where TContext : DbContext
{
    IDbContextFactory<TContext> ContextFactory { get; init; }
    IMapper Mapper { get; init; }

    virtual void OnInitialise()
    {

    }



    //ICurrentUserContext currentUserContext,
    //ILogger<RepositoryBase<TContext>> logger
}