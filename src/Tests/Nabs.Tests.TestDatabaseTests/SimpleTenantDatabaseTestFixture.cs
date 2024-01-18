namespace Nabs.Tests.TestDatabaseTests;

public sealed class SimpleUnsetTenantDatabaseTestFixture(
	IMessageSink diagnosticMessageSink)
		: DatabaseFixtureBase(diagnosticMessageSink)
{
	protected override void ConfigureServices(IServiceCollection services)
	{
		services.TryAddTransient<IApplicationContext>((sp) =>
			ApplicationContextFactory?.Invoke() ?? new ApplicationContext()
			{
				TenantContext = new TenantContext()
				{
					TenantId = Guid.Empty
				}
			});

		services.AddDbContextFactory<SimpleTenantDbContext>(options =>
		{
			//NOTE: This is only a test database, so we can use in-memory
			var connectionString = "Data Source=:memory:";
			var connection = new SqliteConnection(connectionString);
			connection.Open();

			options.UseSqlite(connection);

		});
	}
}

public sealed class SimpleTenantDatabaseTestFixture(
	IMessageSink diagnosticMessageSink)
		: DatabaseFixtureBase(diagnosticMessageSink)
{
	protected override void ConfigureServices(IServiceCollection services)
	{
		services.TryAddTransient<IApplicationContext>((sp) =>
			ApplicationContextFactory?.Invoke() ?? new ApplicationContext()
			{
				TenantContext = new TenantContext()
				{
					TenantId = Guid.NewGuid()
				}
			});

		services.AddDbContextFactory<SimpleTenantDbContext>(options =>
		{
			//NOTE: This is only a test database, so we can use in-memory
			var connectionString = "Data Source=:memory:";
			var connection = new SqliteConnection(connectionString);
			connection.Open();

			options.UseSqlite(connection);

		});
	}
}
