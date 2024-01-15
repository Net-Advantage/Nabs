using System.Text.Json;

namespace Nabs.Tests;

public static class CommonTestDependencies
{
	private static CsvConfiguration? _csvConfiguration;
	private static JsonSerializerOptions? _jsonSerializerOptions;

	internal static CsvConfiguration CsvConfiguration
	{
		get
		{
			_csvConfiguration ??= new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				HasHeaderRecord = true,
				HeaderValidated = null,
				MissingFieldFound = null,
				IgnoreBlankLines = true,
				Delimiter = ",",
				TrimOptions = TrimOptions.Trim
			};

			return _csvConfiguration;
		}
	}
	internal static JsonSerializerOptions JsonSerializerOptions
	{
		get
		{ 			
			_jsonSerializerOptions ??= new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				WriteIndented = true
			};

			return _jsonSerializerOptions;
		}
	}

	public static void RegisterCsvConfiguration(CsvConfiguration csvConfiguration)
	{
		_csvConfiguration = csvConfiguration;
	}

	public static void RegisterJsonSerializerOptions(JsonSerializerOptions jsonSerializerOptions)
	{
		_jsonSerializerOptions = jsonSerializerOptions;
	}
}
