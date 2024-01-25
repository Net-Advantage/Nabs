using Nabs.Serialisation;

namespace Nabs.ActivityFramework.UiManifest;

public class UiManifestResult
{
	public UiManifestTitle Title { get; init; } = default!;
	public List<UiManifestItem> Items { get; set; } = [];

	override public string ToString()
	{
		return DefaultJsonSerializer.Serialize(this);
	}
}