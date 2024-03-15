namespace Nabs.Tests.OpenAiTests;

public sealed class SystemPromptUnitTests(
	ITestOutputHelper testOutputHelper, OpenAiApiClientFixture fixture)
		: FixtureTestBase<OpenAiApiClientFixture>(testOutputHelper, fixture)
{
	private IAiClient _client = default!;

	protected override async Task StartTest()
	{
		_client = TestFixture.ServiceProvider.GetRequiredService<IAiClient>();
		await Task.CompletedTask;
	}

	[Fact(Skip = "Requires secrets")]
	public async Task AddSystemMessage__Success()
	{
		//Objective:
		// Add a system prompt describing the system.
		// Add a user prompt asking for a description of the system.
		// Get the response from the assistant.
		// Assert the assistant's response.

		// Arrange
		var systemPrompt = "You are a 8 year old male child. You like to play with cars.";
		var userPrompt = "Describe yourself.";

		// Act
		_client.AddSystemPrompt(systemPrompt);
		_client.AddUserPrompt(userPrompt);
		await _client.SubmitPrompt();

		// Assert
		_client.ChatContent.Should().NotBeNullOrEmpty();

	}
}
