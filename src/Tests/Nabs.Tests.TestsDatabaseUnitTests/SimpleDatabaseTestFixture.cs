namespace Nabs.Tests.TestDatabaseUnitTests;

public sealed class SimpleDatabaseTestFixture(
    IMessageSink diagnosticMessageSink)
        : DatabaseFixtureBase(diagnosticMessageSink)
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContextFactory<SimpleDbContext>(options =>
        {
            //NOTE: This is only a test database, so we can use in-memory
            var connectionString = "Data Source=:memory:";
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            options.UseSqlite(connection);

        });
    }
}
