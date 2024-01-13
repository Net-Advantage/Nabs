using Xunit.Abstractions;

namespace Nabs.Tests.TestsTests.DataAttributes;

[Collection(nameof(SimpleFixtureCollection))]
public class CsvDataAttributeUnitTests(
	ITestOutputHelper testOutputHelper,
	SimpleTestFixture testFixture) 
	: FixtureTestBase<SimpleTestFixture>(testOutputHelper, testFixture)
{
	[Theory]
	[LoadFromCsvDataAttribute<CsvTestDataModel>(typeof(CsvTestDataModel), "CsvTestData.csv", "|")]
	public void LoadCsvTestDataWithAttribute(CsvTestDataModel item)
	{
		_ = item;

		OutputScenario(item.Scenario.ToString());
	}
}