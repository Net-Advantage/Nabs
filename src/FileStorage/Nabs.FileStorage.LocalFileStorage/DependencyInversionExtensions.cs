using Microsoft.Extensions.DependencyInjection;

namespace Nabs.FileStorage.LocalFileStorage;

public static class DependencyInversionExtensions
{
	public static IServiceCollection AddLocalFileStorageService(this IServiceCollection services)
	{
		services.AddSingleton<IFileStorageService, LocalFileStorageService>();


		return services;
	}

	public static IServiceCollection AddAzureFileStorageService(
		this IServiceCollection services,
		Action<FileStorageSettings> settings)
	{
		services.AddLocalFileStorageService();

		var fileStorageSettings = new FileStorageSettings()
		{

		};

		settings.Invoke(fileStorageSettings);

		return services;
	}
}