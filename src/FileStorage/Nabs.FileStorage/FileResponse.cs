namespace Nabs.FileStorage;

public class FileResponse
{
	public OneOf<string, byte[]> Content { get; set; }
}