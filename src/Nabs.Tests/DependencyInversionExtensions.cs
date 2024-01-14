using System.Text.Json;

namespace Nabs.Tests;

public static class CommonTestDependencies
{
	internal static CsvConfiguration CsvConfiguration { get; private set; } = default!;
	internal static JsonSerializerOptions JsonSerializerOptions { get; private set; } = default!;

	public static void RegisterCsvConfiguration(CsvConfiguration csvConfiguration)
	{
		CsvConfiguration = csvConfiguration ??= new CsvConfiguration(CultureInfo.InvariantCulture)
		{
			HasHeaderRecord = true,
			HeaderValidated = null,
			MissingFieldFound = null,
			IgnoreBlankLines = true,
			Delimiter = ",",
			TrimOptions = TrimOptions.Trim
		};
	}

	public static void RegisterJsonSerializerOptions(JsonSerializerOptions jsonSerializerOptions)
	{
		JsonSerializerOptions = jsonSerializerOptions ??= new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = true
		};
	}
}
