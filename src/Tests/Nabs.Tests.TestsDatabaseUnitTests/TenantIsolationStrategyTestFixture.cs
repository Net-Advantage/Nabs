namespace Nabs.Tests.TestDatabaseUnitTests;

public sealed class TenantIsolationStrategyTestFixture(
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

		services.AddTenantablePersistence<SimpleTenantDbContext>("SimpleTenantDb", ConfigurationRoot);
	}
}
