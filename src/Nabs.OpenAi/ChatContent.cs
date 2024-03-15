using Azure.AI.OpenAI;

namespace Nabs.OpenAi;

public sealed record ChatContent(
	string Prompt,
	ChatRole Role);
