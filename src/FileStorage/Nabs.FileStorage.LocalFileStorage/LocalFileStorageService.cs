namespace Nabs.FileStorage.LocalFileStorage;
public class LocalFileStorageService : IFileStorageService
{
	public async Task<bool> PersistFileAsync(byte[] fileAsBytes)
	{
		return true;
	}

	public async Task<bool> PersistFileAsync(string fileAsString)
	{
		return true;
	}

	public async Task<FileResponse> GetFileAsync(string id)
	{
		var result = new FileResponse();

		return result;
	}

	public async Task<string> GetOneTimeDownloadLinkAsync(string id)
	{
		return string.Empty;
	}

	public async Task<string> GetOneTimeUploadLinkAsync(string id)
	{
		return string.Empty;
	}
}