namespace Nabs.Tests.SerialisationUnitTests;

public class GlobalSettingsUnitTests
{
	[Fact]
	public void GlobalSettingsDefaults()
	{
		ResetStaticMember(typeof(GlobalSettings), "_csvConfiguration");
		ResetStaticMember(typeof(GlobalSettings), "_jsonSerializerOptions");

		GlobalSettings.JsonSerializerOptions.Should().NotBeNull();
		GlobalSettings.CsvConfiguration.Should().NotBeNull();
	}

	[Fact]
	public void GlobalSettingsOverrides()
	{
		ResetStaticMember(typeof(GlobalSettings), "_csvConfiguration");
		ResetStaticMember(typeof(GlobalSettings), "_jsonSerializerOptions");

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

	private static void ResetStaticMember(Type type, string fieldName)
	{
		var field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic);
		field?.SetValue(null, null);
	}
}