namespace Nabs.Tests.OpenAiTests;

public sealed class OpenAiApiClientUnitTest(
	ITestOutputHelper testOutputHelper, OpenAiApiClientFixture fixture)
		: FixtureTestBase<OpenAiApiClientFixture>(testOutputHelper, fixture)
{
	private IAiClient _client = default!;

	protected override async Task StartTest()
	{
		_client = TestFixture.ServiceProvider.GetRequiredService<IAiClient>();
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
		await _client.ClearEmbeddingsContent();
		await _client.CreateEmbeddingsContent(text);

		// Act
		var result = await _client.GetEmbeddingsContent();

		// Assert
		result.Should().NotBeEmpty();
		result.First().Should().Be(text);
	}

	[Fact]
	public async Task SearchContentExact__Success()
	{
		// Arrange
		var text = "Hello, world!";
		await _client.ClearEmbeddingsContent();
		await _client.CreateEmbeddingsContent(text);

		// Act
		var result = await _client.SearchEmbeddingsContent(text);

		// Assert
		result.Should().NotBeEmpty();
		var firstItem = result.First();
		firstItem.content.Should().Be(text);
		firstItem.similarity.Should().BeApproximately(1.0D, 0.1D);
	}

	[Theory]
	[InlineData("Hello, world!", "world", 0.8D, 0.9D)]
	[InlineData("Hello, world!", "hello", 0.8D, 0.9D)]
	[InlineData("Hello, world!", "other", 0.7D, 0.8D)]
	public async Task SearchContent80Percent__Success(string text, string searchText, double min, double max)
	{
		// Arrange
		await _client.ClearEmbeddingsContent();
		await _client.CreateEmbeddingsContent(text);

		// Act
		var result = await _client.SearchEmbeddingsContent(searchText);

		// Assert
		result.Should().NotBeEmpty();
		var firstItem = result.First();
		firstItem.content.Should().Be(text);
		firstItem.similarity.Should().BeInRange(min, max);
	}

}