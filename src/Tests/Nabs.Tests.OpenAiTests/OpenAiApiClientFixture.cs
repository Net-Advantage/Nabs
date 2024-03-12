using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nabs.OpenAi;
using Nabs.Tests.Fixtures;
using Xunit.Abstractions;

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
