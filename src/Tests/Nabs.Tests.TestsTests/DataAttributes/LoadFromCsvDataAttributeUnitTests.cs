namespace Nabs.Tests.TestsTests.DataAttributes;

[Collection(nameof(SimpleFixtureCollection))]
public class LoadFromCsvDataAttributeUnitTests(
	ITestOutputHelper testOutputHelper,
	SimpleTestFixture testFixture) 
	: FixtureTestBase<SimpleTestFixture>(testOutputHelper, testFixture)
{
	[Theory]
	[LoadFromCsvDataAttribute<TestDataModel>(typeof(TestDataModel), "CsvTestData.csv")]
	public void LoadCsvTestDataWithAttribute(TestDataModel item)
	{
		_ = item;

		OutputScenario(item.Scenario.ToString());
	}
}