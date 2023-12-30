namespace Nabs.Tests.PersistenceTests.Fixtures;

public sealed class DataLoaderFixture(IMessageSink diagnosticMessageSink) 
	: TestConfigurationFixtureBase(diagnosticMessageSink)
{
	protected override void ConfigureServices(IServiceCollection services)
	{
		services.AddAutoMapper(typeof(DataLoaderFixture));

		services.AddDbContextFactory<TestDbContext>(options =>
		{
			var connectionString = "Data Source=:memory:";
			var keepAliveConnection = new SqliteConnection(connectionString);
			keepAliveConnection.Open();

			options.UseSqlite(keepAliveConnection);
		});

		services.AddTransient<IRelationalRepositoryOptions<TestDbContext>, RelationalRepositoryOptions<TestDbContext>>();
		services.AddScoped<IRelationalRepository<TestDbContext>, RelationalRepository<TestDbContext>>();
	}
}