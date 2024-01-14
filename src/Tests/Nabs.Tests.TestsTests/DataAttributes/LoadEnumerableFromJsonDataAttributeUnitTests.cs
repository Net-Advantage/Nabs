namespace Nabs.Tests.TestsTests.DataAttributes;

[Collection(nameof(SimpleFixtureCollection))]
public class LoadEnumerableFromJsonDataAttributeUnitTests(
	ITestOutputHelper testOutputHelper,
	SimpleTestFixture testFixture)
	: FixtureTestBase<SimpleTestFixture>(testOutputHelper, testFixture)
{
	[Theory]
	[LoadEnumerableFromJsonDataAttribute<TestDataModel>(typeof(TestDataModel), "JsonTestData.json")]
	public void LoadCsvTestDataWithAttribute(TestDataModel item)
	{
		_ = item;

		OutputScenario(item.Scenario.ToString());
	}
}