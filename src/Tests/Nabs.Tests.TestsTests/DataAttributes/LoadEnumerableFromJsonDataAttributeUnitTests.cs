namespace Nabs.Tests.TestsTests.DataAttributes;

[Collection(nameof(SimpleFixtureCollection))]
public class LoadEnumerableFromJsonDataAttributeUnitTests(
	ITestOutputHelper testOutputHelper,
	SimpleTestFixture testFixture)
	: FixtureTestBase<SimpleTestFixture>(testOutputHelper, testFixture)
{
	[Theory]
	[LoadEnumerableFromJsonDataAttribute<TestDataModel>(typeof(TestDataModel), "JsonTestData.json")]
	public void LoadJsonTestDataWithAttribute(TestDataModel item)
	{
		_ = item;

		OutputScenario(item.Scenario.ToString());
	}

	
	[Fact]
	public void LoadJsonBadTestDataWithAttribute()
	{
		// Arrange
		var attribute = new LoadEnumerableFromJsonDataAttribute<TestDataModel>(typeof(TestDataModel), "JsonBadTestData.json");
		var methodInfo = typeof(LoadEnumerableFromJsonDataAttributeUnitTests).GetMethod(nameof(LoadJsonBadTestDataWithAttribute))!;

		// Act
		var action = () => attribute.GetData(methodInfo).ToList();

		// Assert
		var exception = action.Should().Throw<Exception>();
		exception.Subject.ToArray()[0].Source.Should().Be("System.Text.Json");
	}

	[Fact]
	public void LoadJsonMissingFileWithAttribute()
	{
		// Arrange
		var attribute = new LoadEnumerableFromJsonDataAttribute<TestDataModel>(typeof(TestDataModel), "MissingFile.json");
		var methodInfo = typeof(LoadEnumerableFromJsonDataAttributeUnitTests).GetMethod(nameof(LoadJsonMissingFileWithAttribute))!;

		// Act
		var action = () => attribute.GetData(methodInfo).ToList();

		// Assert
		var exception = action.Should().Throw<Exception>();
		var subject = exception.Subject.ToArray()[0];
		subject.Source.Should().Be("Nabs.Tests");
		subject.Message.Should().Be("No resource info items found.");
	}
}