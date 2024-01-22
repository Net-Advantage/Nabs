namespace Nabs.Tests.Fixtures;

public abstract class ConfigurationTestFixtureBase(
	IMessageSink diagnosticMessageSink) 
	: TestFixtureBase(diagnosticMessageSink)
{
	public IConfigurationRoot ConfigurationRoot { get; private set; } = default!;
	public IServiceProvider ServiceProvider { get; private set; } = default!;
	public IServiceScope ServiceScope {get; private set; } = default!;
	
	protected virtual void ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
	{

	}

	protected virtual void ConfigureServices(IServiceCollection services)
	{
	}

	public override void Initialise()
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
}