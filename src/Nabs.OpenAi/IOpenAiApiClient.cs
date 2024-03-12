namespace Nabs.OpenAi;

public interface IOpenAiApiClient
{
	Task ClearEmbeddingsAsync();
	Task CreateEmbeddingsAsync(string text);
	Task<List<string>> GetEmbeddingsContentAsync();
	Task<List<(string content, double similarity)>> SearchContentAsync(string searchText, int topN = 5);
}

