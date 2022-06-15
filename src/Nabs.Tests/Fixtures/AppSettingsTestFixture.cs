namespace Nabs.Tests.Fixtures;

public abstract class AppSettingsTestFixture : TestFixtureBase
{
	protected override void ConfigureConfiguration(
		IConfigurationBuilder configurationBuilder)
	{
		configurationBuilder
			.AddJsonFile("appsettings.json", false);
	}

	protected override void ConfigureServices(
		IServiceCollection services)
	{

	}
}