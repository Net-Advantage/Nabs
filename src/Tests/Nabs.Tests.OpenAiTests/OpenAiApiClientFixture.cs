using Microsoft.Extensions.Configuration;
using Nabs.Tests.Fixtures;

namespace Nabs.Tests.OpenAiTests;

public sealed class OpenAiApiClientFixture(
	IMessageSink diagnosticMessageSink)
	: ConfigurationTestFixtureBase(diagnosticMessageSink)
{
	protected override void ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
	{

	}

	protected override void ConfigureServices(IServiceCollection services)
	{
		services.AddOpenAiApiClient(ConfigurationRoot);
	}
}
