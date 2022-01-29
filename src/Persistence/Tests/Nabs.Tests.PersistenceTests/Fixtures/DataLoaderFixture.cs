using Nabs.Tests.PersistenceTests.TestData;

namespace Nabs.Tests.PersistenceTests.Fixtures;

public sealed class DataLoaderFixture : TestFixtureBase
{
    private readonly DataLoader _dataLoader = new DataLoader();

    public DataLoaderFixture()
    {
        
    }

    protected override void ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .AddJsonFile("appsettings.json", false)
            .AddUserSecrets(typeof(DataLoaderFixture).Assembly, false);
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContextFactory<TestDbContext>(_ =>
        {
            var connectionString = "Filename=:memory:";
            var sqliteConnection = new SqliteConnection(connectionString);
            sqliteConnection.Open();
            _.UseSqlite(sqliteConnection, options =>
            {
                options.MinBatchSize(1);
                options.MaxBatchSize(25); //TODO: DWS: Make configurable
            });
        });
        services.AddSingleton<IRelationalRepositoryOptions<TestDbContext>, RelationalRepositoryOptions<TestDbContext>>();
        services.AddTransient<IRepository<TestDbContext>, RelationalRepository<TestDbContext>>();
    }

    public override async Task EnsureDatabaseLoaderAsync()
    {
        var testRepository = ServiceScope.ServiceProvider
            .GetRequiredService<IRepository<TestDbContext>>();

        
        await _dataLoader.LoadAsync(testRepository);
    }
}