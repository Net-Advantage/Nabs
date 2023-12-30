namespace Nabs.FileStorage.LocalFileStorage;
public class LocalFileStorageService : IFileStorageService
{
	public async Task<bool> PersistFileAsync(byte[] fileAsBytes)
	{
		return await Task.FromResult(true);
	}

	public async Task<bool> PersistFileAsync(string fileAsString)
	{
		return await Task.FromResult(true);
	}

	public async Task<FileResponse> GetFileAsync(string id)
	{
		var result = new FileResponse();

		return await Task.FromResult(result);
	}

	public async Task<string> GetOneTimeDownloadLinkAsync(string id)
	{
		return await Task.FromResult(string.Empty);
	}

	public async Task<string> GetOneTimeUploadLinkAsync(string id)
	{
		return await Task.FromResult(string.Empty);
	}
}