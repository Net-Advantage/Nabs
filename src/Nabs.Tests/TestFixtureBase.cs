namespace Nabs.Tests;

public abstract class TestFixtureBase : IDisposable
{
    protected TestFixtureBase()
    {
        Initialise();
    }

    public IConfigurationRoot ConfigurationRoot { get; private set; }
    public IServiceScope ServiceScope { get; private set; }

    protected void Initialise()
    {
        var configurationBuilder = new ConfigurationBuilder();
        ConfigureConfiguration(configurationBuilder);
        ConfigurationRoot = configurationBuilder.Build();

        var services = new ServiceCollection();

        ConfigureServices(services);

        ServiceScope = services
            .BuildServiceProvider(
                new ServiceProviderOptions
                {
                    ValidateScopes = true,
                    ValidateOnBuild = true
                })
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
    }

    protected virtual void ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
    {

    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
    }

    public virtual async Task EnsureDatabaseLoaderAsync()
    {
        await Task.CompletedTask;
    }

    public void Dispose()
    {
        ConfigurationRoot = null;
        ServiceScope.Dispose();
    }
}