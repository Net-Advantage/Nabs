namespace Nabs.Persistence;

public static class DependencyInversionExtensions
{
    public static IServiceCollection AddTenantablePersistence<TDbContext>(
        this IServiceCollection services,
        string databaseNamePrefix,
        IConfigurationRoot configuration)
        where TDbContext : DbContext, ITenantableDbContext
    {
        services.AddSingleton<ITenantableDbContextFactory<TDbContext>>(
            new TenantableDbContextFactory<TDbContext>(databaseNamePrefix, configuration));

        return services;
    }
}
