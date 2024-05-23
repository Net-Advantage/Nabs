using Azure.AI.OpenAI;

namespace Nabs.OpenAi.Extensions;

internal static class EmbeddingsContentExtensions
{
    internal static async Task ClearEmbeddingsContentAsync(this AiClient client)
    {
        client.ContentVectors.Clear();
        await Task.CompletedTask;
    }

    internal static async Task CreateEmbeddingsContentAsync(this AiClient client, string text)
    {
        var embeddingsOptions = new EmbeddingsOptions()
        {
            DeploymentName = client.Settings.EmbeddingsDeploymentName,
            Input = { text },
        };
        var response = await client.OpenAiClient
            .GetEmbeddingsAsync(embeddingsOptions);
        client.ContentVectors.TryAdd(text, response.Value);
    }

    internal static async Task<List<string>> GetEmbeddingsContentAsync(this AiClient client)
    {
        var result = new List<string>();
        foreach (var _contentVector in client.ContentVectors)
        {
            result.Add(_contentVector.Key);
        }
        return await Task.FromResult(result);
    }
}