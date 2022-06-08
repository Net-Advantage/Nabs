using Microsoft.Extensions.DependencyInjection;

namespace Nabs.FileStorage.AzureFileStorage;

public static class DependencyInversionExtensions
{
	public static IServiceCollection AddAzureFileStorageService(this IServiceCollection services)
	{
		services.AddSingleton<IFileStorageService, AzureFileStorageService>();


		return services;
	}

	public static IServiceCollection AddAzureFileStorageService(
		this IServiceCollection services,
		Action<FileStorageSettings> settings)
	{
		services.AddAzureFileStorageService();

		var fileStorageSettings = new FileStorageSettings()
		{

		};

		settings.Invoke(fileStorageSettings);

		return services;
	}
}