namespace Nabs.Tests.ResourcesUnitTests;

public class EmbeddedResourceLoaderUnitTest
{
	[Fact]
	public void RunToSuccess()
	{
		// Arrange
		var types = new[] { typeof(EmbeddedResourceLoaderUnitTest) };
		var loader = new EmbeddedResourceLoader(types);

		// Act
		var result = loader.GetResourceInfoItems(x => x.Path.EndsWith(".TestEmbeddedResource.txt"));

		// Assert
		result.Should().NotBeEmpty();

	}

	[Fact]
	public void RunToFailure()
	{
		// Arrange
		var types = new[] { typeof(EmbeddedResourceLoaderUnitTest) };
		var loader = new EmbeddedResourceLoader(types);

		// Act
		var result = loader.GetResourceInfoItems(x => x.Path.EndsWith(".NotAValidFile.txt"));

		// Assert
		result.Should().BeEmpty();

	}
}