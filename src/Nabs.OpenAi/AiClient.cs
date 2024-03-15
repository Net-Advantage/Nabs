using Azure.AI.OpenAI;
using Nabs.OpenAi.Extensions;

namespace Nabs.OpenAi;

internal sealed class AiClient : IAiClient
{
	public AiClient(OpenAiApiClientSettings settings)
	{
		ChatCompletionsOptions = new ChatCompletionsOptions()
		{
			DeploymentName = settings.OpenAIModel,
			ResponseFormat = ChatCompletionsResponseFormat.Text,
		};

		Settings = settings;
		OpenAiClient = new OpenAIClient(settings.OpenAIClientKey);
	}

	public OpenAiApiClientSettings Settings { get; }
	public OpenAIClient OpenAiClient { get; }
	public Dictionary<string, Embeddings> ContentVectors { get; } = [];
	public List<ChatContent> ChatContent { get; } = [];
	public ChatCompletionsOptions ChatCompletionsOptions { get; }

	public async Task ClearEmbeddingsContent()
	{
		await this.ClearEmbeddingsContentAsync();
	}

	public async Task CreateEmbeddingsContent(string text)
	{
		await this.CreateEmbeddingsContentAsync(text);
	}

	public async Task<List<string>> GetEmbeddingsContent()
	{
		return await this.GetEmbeddingsContentAsync();
	}

	public async Task<List<(string content, double similarity)>> SearchEmbeddingsContent(
		string searchText, int topN = 5)
	{
		return await this.SearchEmbeddingsContentAsync(searchText, topN);
	}

	public void AddSystemPrompt(string prompt)
	{
		this.AddSystemPromptAsync(prompt);
	}

	public void AddUserPrompt(string prompt)
	{
		this.AddUserPromptAsync(prompt);
	}

	public async Task SubmitPrompt()
	{
		await this.SubmitPromptAsync();
	}

	
}

