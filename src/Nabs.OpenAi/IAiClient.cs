namespace Nabs.OpenAi;

public interface IAiClient
{
	List<ChatContent> ChatContent { get; }

	Task ClearEmbeddingsContent();
	Task CreateEmbeddingsContent(string text);
	Task<List<string>> GetEmbeddingsContent();
	Task<List<(string content, double similarity)>> SearchEmbeddingsContent(string searchText, int topN = 5);
	void AddSystemPrompt(string prompt);
	void AddUserPrompt(string prompt);
	Task SubmitPrompt();
}

