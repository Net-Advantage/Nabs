namespace Nabs.Serialisation;

public class GlobalSettings
{
    private static CsvConfiguration? _csvConfiguration;
    private static JsonSerializerOptions? _jsonSerializerOptions;

    public static CsvConfiguration CsvConfiguration
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
    public static JsonSerializerOptions JsonSerializerOptions
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
