using Xunit.Abstractions;

namespace Nabs.Tests.ActivityFrameworkUnitTests.UiManifest;

public sealed class UiManifestUnitTests(ITestOutputHelper outputHelper)
{
	private readonly ITestOutputHelper _outputHelper = outputHelper;

	[Fact]
	public void RenderUiManifestTest()
	{
		// Arrange
		var uiManifest = new MyActivityStateUiManifest();
		
		// Act
		var result = uiManifest.Render();

		// Assert
		result.Should().NotBeNull();
		result.Title.Should().NotBeNull();
		result.Items.Should().NotBeNullOrEmpty();

		_outputHelper.WriteLine(result.ToString());
	}
}
