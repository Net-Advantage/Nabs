namespace Nabs.OpenAi;

public sealed class OpenAiApiClientSettings
{
	public string OpenAIClientKey { get; set; } = default!;
	public string OpenAIModel { get; set; } = default!;
	public string EmbeddingsDeploymentName { get; set; } = default!;
}

