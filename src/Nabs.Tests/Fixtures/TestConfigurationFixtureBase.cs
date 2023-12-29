namespace Nabs.Tests.Fixtures;

public abstract class TestConfigurationFixtureBase : TestFixtureBase
{

	protected TestConfigurationFixtureBase(IMessageSink diagnosticMessageSink)
		: base(diagnosticMessageSink)
	{
		var superTestFixtureAssembly = GetType().Assembly;

		var configurationBuilder = new ConfigurationBuilder();
		configurationBuilder
			.AddJsonFile("appsettings.json", false)
			.AddUserSecrets(superTestFixtureAssembly, true);

		ConfigureConfiguration(configurationBuilder);
		ConfigurationRoot = configurationBuilder.Build();

		var services = new ServiceCollection();

		ConfigureServices(services);

		ServiceProvider = services
			.BuildServiceProvider(
				new ServiceProviderOptions
				{
					ValidateScopes = true,
					ValidateOnBuild = true
				});

		ServiceScope = ServiceProvider
			.GetRequiredService<IServiceScopeFactory>()
			.CreateScope();
	}

	public IConfigurationRoot ConfigurationRoot { get; }
	public IServiceProvider ServiceProvider { get; }
	public IServiceScope ServiceScope {get; }
	
	protected virtual void ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
	{

	}

	protected virtual void ConfigureServices(IServiceCollection services)
	{
	}
}