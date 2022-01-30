using Nabs.Tests.PersistenceTests.TestData;

namespace Nabs.Tests.PersistenceTests.Fixtures;

public sealed class DataLoaderFixture : TestFixtureBase
{
    public TestDbContextDataLoader? TestDbContextDataLoader { get; private set; }

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
            const string connectionString = "Filename=:memory:";
            var sqliteConnection = new SqliteConnection(connectionString);
            sqliteConnection.Open();
            _.UseSqlite(sqliteConnection, options =>
            {
                options.MinBatchSize(1);
                options.MaxBatchSize(25); //TODO: DWS: Make configurable
            });
        });
        services.AddSingleton<IRelationalRepositoryOptions<TestDbContext>, RelationalRepositoryOptions<TestDbContext>>();
        services.AddTransient<IRelationalRepository<TestDbContext>, RelationalRepository<TestDbContext>>();
    }

    public override async Task EnsureDatabaseLoaderAsync()
    {
        if (TestDbContextDataLoader != null)
        {
            return;
        }

        var testRepository = ServiceScope.ServiceProvider
            .GetRequiredService<IRelationalRepository<TestDbContext>>();

        TestDbContextDataLoader = new TestDbContextDataLoader(testRepository);
        await TestDbContextDataLoader.LoadAsync();
    }
}