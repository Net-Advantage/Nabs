using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nabs.OpenAi;

public static class DependencyInversionExtensions
{
	public static IServiceCollection AddOpenAiApiClient(this IServiceCollection services, IConfigurationRoot configuration)
	{
		var configSection = configuration.GetRequiredSection(nameof(OpenAiApiClientSettings));
		services.AddSingleton(a => configSection.Get<OpenAiApiClientSettings>()!);

		services.AddSingleton<IOpenAiApiClient, OpenAiApiClient>();

		return services;
	}
}
