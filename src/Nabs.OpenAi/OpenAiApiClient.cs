namespace Nabs.OpenAi;

public sealed class OpenAiApiClient(HttpClient client, OpenAiApiClientSettings settings)
{
	private readonly HttpClient _client = client;
	private readonly OpenAiApiClientSettings _settings = settings;


}

public sealed class OpenAiApiClientSettings
{

	public string OpenAiKey { get; set; } = default!;
	public string OpenAiApiEndpoint { get; set; } = default!;
}

