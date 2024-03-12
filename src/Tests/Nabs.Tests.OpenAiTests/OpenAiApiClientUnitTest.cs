namespace Nabs.Tests.OpenAiTests;

public sealed class OpenAiApiClientUnitTest(
	ITestOutputHelper testOutputHelper, OpenAiApiClientFixture fixture)
		: FixtureTestBase<OpenAiApiClientFixture>(testOutputHelper, fixture)
{
	private IOpenAiApiClient _client = default!;

	protected override async Task StartTest()
	{
		_client = TestFixture.ServiceProvider.GetRequiredService<IOpenAiApiClient>();
		await Task.CompletedTask;
	}


	[Fact]
	public void ClientInstantiated__Success()
	{
		// Assert
		_client.Should().NotBeNull();
	}

	[Fact]
	public async Task EmbeddingsCreated__Success()
	{
		// Arrange
		var text = "Hello, world!";
		await _client.ClearEmbeddingsAsync();
		await _client.CreateEmbeddingsAsync(text);

		// Act
		var result = await _client.GetEmbeddingsContentAsync();

		// Assert
		result.Should().NotBeEmpty();
		result.First().Should().Be(text);
	}

	[Fact]
	public async Task SearchContentExact__Success()
	{
		// Arrange
		var text = "Hello, world!";
		await _client.ClearEmbeddingsAsync();
		await _client.CreateEmbeddingsAsync(text);

		// Act
		var result = await _client.SearchContentAsync(text);

		// Assert
		result.Should().NotBeEmpty();
		var firstItem = result.First();
		firstItem.content.Should().Be(text);
		firstItem.similarity.Should().Be(1.0D);
	}

	[Theory]
	[InlineData("Hello, world!", "world", 0.8D, 0.9D)]
	[InlineData("Hello, world!", "hello", 0.8D, 0.9D)]
	[InlineData("Hello, world!", "other", 0.7D, 0.8D)]
	public async Task SearchContent80Pcnt__Success(string text, string searchText, double min, double max)
	{
		// Arrange
		await _client.ClearEmbeddingsAsync();
		await _client.CreateEmbeddingsAsync(text);

		// Act
		var result = await _client.SearchContentAsync(searchText);

		// Assert
		result.Should().NotBeEmpty();
		var firstItem = result.First();
		firstItem.content.Should().Be(text);
		firstItem.similarity.Should().BeInRange(min, max);
	}

}