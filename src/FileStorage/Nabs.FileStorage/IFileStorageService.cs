namespace Nabs.FileStorage
{
	public interface IFileStorageService
	{
		Task<bool> PersistFileAsync(byte[] fileAsBytes);
		Task<bool> PersistFileAsync(string fileAsString);

		Task<FileResponse> GetFileAsync(string id);

		Task<string> GetOneTimeDownloadLinkAsync(string id);
		Task<string> GetOneTimeUploadLinkAsync(string id);
	}
}