namespace Nabs.Tests.ResourcesUnitTests;

[Collection(nameof(SimpleFixtureCollection))]
public sealed class EmbeddedResourceLoaderUnitTest
	: FixtureTestBase<SimpleTestFixture>
{
	const string _txtEmbeddedResourcePath = ".TestEmbeddedResource.txt";
	const string _txtMissingEmbeddedResourcePath = ".MissingFile.txt";
	const string _jsonArrayEmbeddedResourcePath = ".TestArrayEmbeddedResource.json";
	const string _jsonObjectEmbeddedResourcePath = ".TestObjectEmbeddedResource.json";
	const string _jsonMissingEmbeddedResourcePath = ".MissingFile.json";
	const string _pngEmbeddedResourcePath = ".nabs_logo.png";
	const string _pngMissingEmbeddedResourcePath = ".MissingFile.png";
	const string _multiTestEmbeddedResourcePath = ".MultiTestEmbeddedResource.txt";

	private readonly EmbeddedResourceLoader _resourceLoader;

	public EmbeddedResourceLoaderUnitTest(
		ITestOutputHelper testOutputHelper, 
		SimpleTestFixture testFixture) 
		: base(testOutputHelper, testFixture)
	{
		var types = new[] { typeof(EmbeddedResourceLoaderUnitTest) };
		_resourceLoader = new EmbeddedResourceLoader(types);
	}

	[Fact]
	public void GetResourceInfoItems_RunToSuccess()
	{
		// Arrange
		// Act
		var result = _resourceLoader.GetResourceInfoItems(x => x.Path.EndsWith(_txtEmbeddedResourcePath));

		// Assert
		result.Should().NotBeEmpty();
	}

	[Fact]
	public void GetResourceInfoItems_RunToFailure()
	{
		// Arrange
		// Act
		var result = _resourceLoader.GetResourceInfoItems(x => x.Path.EndsWith(_txtMissingEmbeddedResourcePath));

		// Assert
		result.Should().BeEmpty();
	}

	[Fact]
	public void GetResourceStreamContent_RunToSuccess()
	{
		// Arrange
		// Act
		var result = _resourceLoader.GetResourceStreamContent(x => x.Path.EndsWith(_txtEmbeddedResourcePath));

		// Assert
		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void GetResourceStreamContent_RunToFailure()
	{
		// Arrange
		// Act
		var result = _resourceLoader.GetResourceStreamContent(x => x.Path.EndsWith(_txtMissingEmbeddedResourcePath));

		// Assert
		result.IsFaulted.Should().BeTrue();
	}

	[Fact]
	public void GetResourceBytesContent_RunToSuccess()
	{
		// Arrange
		// Act
		var result = _resourceLoader.GetResourceBytesContent(x => x.Path.EndsWith(_pngEmbeddedResourcePath));

		// Assert
		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void GetResourceBytesContent_RunToFailure()
	{
		// Arrange
		// Act
		var result = _resourceLoader.GetResourceBytesContent(x => x.Path.EndsWith(_pngMissingEmbeddedResourcePath));

		// Assert
		result.IsFaulted.Should().BeTrue();
	}

	[Fact]
	public void GetResourceTextContent_RunToSuccess()
	{
		// Arrange
		// Act
		var result = _resourceLoader.GetResourceTextContent(x => x.Path.EndsWith(_txtEmbeddedResourcePath));

		// Assert
		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void GetResourceTextContent_RunToFailure()
	{
		// Arrange
		// Act
		var result = _resourceLoader.GetResourceTextContent(x => x.Path.EndsWith(_txtMissingEmbeddedResourcePath));

		// Assert
		result.IsFaulted.Should().BeTrue();
	}

	[Fact]
	public void GetResourceTextContent_PredicateWithMultipleResults_RunToFailure()
	{
		// Arrange
		// Act
		var result = _resourceLoader.GetResourceTextContent(x => x.Path.EndsWith(_multiTestEmbeddedResourcePath));

		// Assert
		result.IsFaulted.Should().BeTrue();
	}	
}