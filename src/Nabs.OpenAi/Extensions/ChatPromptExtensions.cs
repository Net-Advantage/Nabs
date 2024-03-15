using Azure.AI.OpenAI;
using Azure;

namespace Nabs.OpenAi.Extensions;

internal static class ChatPromptExtensions
{
	internal static void AddSystemPromptAsync(this AiClient client, string prompt)
	{
		client.ChatContent.Add(new ChatContent(prompt, ChatRole.System));

		client.ChatCompletionsOptions.Messages
			.Add(new ChatRequestSystemMessage(prompt));
	}

	internal static void AddUserPromptAsync(this AiClient client, string prompt)
	{
		client.ChatContent.Add(new ChatContent(prompt, ChatRole.User));

		client.ChatCompletionsOptions.Messages
			.Add(new ChatRequestUserMessage(prompt));
	}

	internal static async Task SubmitPromptAsync(this AiClient client)
	{
		var result = await client.OpenAiClient.GetChatCompletionsAsync(client.ChatCompletionsOptions);
		foreach (var choice in result.Value.Choices)
		{
			client.ChatContent.Add(new ChatContent(choice.Message.Content, ChatRole.Assistant));
		}
	}
}
