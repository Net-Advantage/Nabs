using Azure.AI.OpenAI;
namespace Nabs.OpenAi;

internal sealed class OpenAiApiClient : IOpenAiApiClient
{
	private readonly OpenAIClient _openAiClient;
	private readonly OpenAiApiClientSettings _settings;
	private readonly ChatCompletionsOptions _chatCompletionsOptions;
	private readonly Dictionary<string, Embeddings> _contentVectors = [];

	public OpenAiApiClient(OpenAiApiClientSettings settings)
	{
		_chatCompletionsOptions = new ChatCompletionsOptions()
		{
			DeploymentName = settings.OpenAIModel,
			ResponseFormat = ChatCompletionsResponseFormat.Text,
		};

		_settings = settings;
		_openAiClient = new OpenAIClient(settings.OpenAIClientKey);
	}

	public async Task ClearEmbeddingsAsync()
	{
		_contentVectors.Clear();
		await Task.CompletedTask;
	}

	public async Task CreateEmbeddingsAsync(string text)
	{
		var embeddingsOptions = new EmbeddingsOptions()
		{
			DeploymentName = _settings.EmbeddingsDeploymentName,
			Input = { text },
		};
		var response = await _openAiClient
			.GetEmbeddingsAsync(embeddingsOptions);
		_contentVectors.TryAdd(text, response.Value);
	}

	public async Task<List<string>> GetEmbeddingsContentAsync()
	{
		var result = new List<string>();
		foreach (var _contentVector in _contentVectors)
		{
			result.Add(_contentVector.Key);
		}
		return await Task.FromResult(result);
	}

	public async Task<List<(string content, double similarity)>> SearchContentAsync(string searchText, int topN = 5)
	{
		var searchEmbedding = await _openAiClient.GetEmbeddingsAsync(new EmbeddingsOptions()
		{
			DeploymentName = _settings.EmbeddingsDeploymentName,
			Input = { searchText },
		});
		var searchVector = searchEmbedding.Value.Data.First().Embedding.ToArray();

		var scoredItems = _contentVectors.Select(contentVector => new
		{
			ContentVector = contentVector,
			Similarity = CosineSimilarity(contentVector.Value.Data.First().Embedding.ToArray(), searchVector)
		})
		.OrderByDescending(x => x.Similarity)
		.Take(topN)
		.Select(x => (x.ContentVector.Key, x.Similarity))
		.ToList();
		return scoredItems;
	}

	private static double CosineSimilarity(float[] vectorA, float[] vectorB)
	{
		double dotProduct = 0.0;
		double normA = 0.0;
		double normB = 0.0;
		for (int i = 0; i < vectorA.Length; i++)
		{
			dotProduct += vectorA[i] * vectorB[i];
			normA += vectorA[i] * vectorA[i];
			normB += vectorB[i] * vectorB[i];
		}
		return dotProduct / (Math.Sqrt(normA) * Math.Sqrt(normB));
	}
}

