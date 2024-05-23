using Azure.AI.OpenAI;

namespace Nabs.OpenAi.Extensions;

internal static class SearchEmbeddingsContentExtensions
{
    internal static async Task<List<(string content, double similarity)>> SearchEmbeddingsContentAsync(
        this AiClient client, string searchText, int topN = 5)
    {
        var searchEmbedding = await client.OpenAiClient.GetEmbeddingsAsync(new EmbeddingsOptions()
        {
            DeploymentName = client.Settings.EmbeddingsDeploymentName,
            Input = { searchText },
        });
        var searchVector = searchEmbedding.Value.Data[0].Embedding.ToArray();

        var scoredItems = client.ContentVectors.Select(contentVector => new
        {
            ContentVector = contentVector,
            Similarity = CosineSimilarity(contentVector.Value.Data[0].Embedding.ToArray(), searchVector)
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
