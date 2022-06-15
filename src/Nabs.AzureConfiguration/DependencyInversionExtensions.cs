using Azure.Identity;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nabs.AzureConfiguration
{
	public static class DependencyInversionExtensions
	{
		public static void AddNabsAzureConfiguration(
			this IServiceCollection services,
			IConfigurationRoot configurationRoot)
		{
			services.AddAzureClients(builder =>
			{
				var azureClientOptions = configurationRoot
					.GetSection("AzureClientOptions");
				builder.ConfigureDefaults(azureClientOptions);

				builder.UseCredential(new EnvironmentCredential());


			});
		}
	}
}