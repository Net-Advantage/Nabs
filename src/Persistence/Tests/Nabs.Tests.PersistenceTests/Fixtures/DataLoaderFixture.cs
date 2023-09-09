namespace Nabs.Tests.PersistenceTests.Fixtures;

public sealed class DataLoaderFixture : TestFixtureBase
{
	protected override void ConfigureConfiguration(
		IConfigurationBuilder configurationBuilder)
	{
		configurationBuilder
			.AddUserSecrets(typeof(DataLoaderFixture).Assembly, false);
	}

	protected override void ConfigureServices(
		IServiceCollection services)
	{
		services.AddAutoMapper(typeof(DataLoaderFixture));

		services.AddTransient<IRelationalRepositoryOptions<TestDbContext>, RelationalRepositoryOptions<TestDbContext>>();
		services.AddScoped<IRelationalRepository<TestDbContext>, RelationalRepository<TestDbContext>>();
	}
}