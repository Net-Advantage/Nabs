using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Nabs.OpenAi;

[ExcludeFromCodeCoverage]
public static class DependencyInversionExtensions
{
	public static IServiceCollection AddOpenAiApiClient(this IServiceCollection services, IConfigurationRoot configuration)
	{
		var configSection = configuration.GetRequiredSection(nameof(OpenAiApiClientSettings));
		services.AddSingleton(a => configSection.Get<OpenAiApiClientSettings>()!);

		services.AddSingleton<IAiClient, AiClient>();

		return services;
	}
}
