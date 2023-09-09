namespace Nabs.FileStorage.AzureFileStorage;

public class AzureFileStorageService : IFileStorageService
{
	public async Task<bool> PersistFileAsync(byte[] fileAsBytes)
	{
		await Task.CompletedTask;
		return true;
	}

	public async Task<bool> PersistFileAsync(string fileAsString)
	{
		await Task.CompletedTask;
		return true;
	}

	public async Task<FileResponse> GetFileAsync(string id)
	{
		var result = new FileResponse();
		await Task.CompletedTask;
		return result;
	}

	public async Task<string> GetOneTimeDownloadLinkAsync(string id)
	{
		await Task.CompletedTask;
		return string.Empty;
	}

	public async Task<string> GetOneTimeUploadLinkAsync(string id)
	{
		await Task.CompletedTask;
		return string.Empty;
	}
}