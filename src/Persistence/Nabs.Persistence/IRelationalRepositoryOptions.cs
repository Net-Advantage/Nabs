

namespace Nabs.Persistence;

public interface IRelationalRepositoryOptions<TContext>
    where TContext : DbContext
{
    IDbContextFactory<TContext> ContextFactory { get; init; }

    virtual void OnInitialise()
    {
        
    }
    


    //ICurrentUserContext currentUserContext,
    //ILogger<RepositoryBase<TContext>> logger
}