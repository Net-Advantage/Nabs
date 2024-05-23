namespace Nabs.Tests.SerialisationUnitTests;

public sealed class GlobalSettingsUnitTests : BaseSerialisationUnitTest
{
    [Fact]
    public void GlobalSettingsDefaults()
    {
        GlobalSettings.JsonSerializerOptions.Should().NotBeNull();
        GlobalSettings.CsvConfiguration.Should().NotBeNull();
    }

    [Fact]
    public void GlobalSettingsOverrides()
    {
        GlobalSettings.RegisterJsonSerializerOptions(new JsonSerializerOptions
        {
            WriteIndented = true
        });

        GlobalSettings.RegisterCsvConfiguration(new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
            MissingFieldFound = null,
            IgnoreBlankLines = true,
            Delimiter = "|",
            TrimOptions = TrimOptions.Trim
        });

        GlobalSettings.JsonSerializerOptions.Should().NotBeNull();
        GlobalSettings.CsvConfiguration.Should().NotBeNull();
    }
}