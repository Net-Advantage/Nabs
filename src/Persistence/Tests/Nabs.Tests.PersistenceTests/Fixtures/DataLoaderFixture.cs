namespace Nabs.Tests.PersistenceTests.Fixtures;

public sealed class DataLoaderFixture(IMessageSink diagnosticMessageSink) 
	: TestConfigurationFixtureBase(diagnosticMessageSink)
{
	protected override void ConfigureServices(IServiceCollection services)
	{
		services.AddAutoMapper(typeof(DataLoaderFixture));

		services.AddTransient<IRelationalRepositoryOptions<TestDbContext>, RelationalRepositoryOptions<TestDbContext>>();
		services.AddScoped<IRelationalRepository<TestDbContext>, RelationalRepository<TestDbContext>>();
	}
}