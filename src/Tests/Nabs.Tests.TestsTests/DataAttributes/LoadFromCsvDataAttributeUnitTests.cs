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

	[Fact]
	public void LoadCsvBadTestDataWithAttribute()
	{
		// Arrange
		var attribute = new LoadFromCsvDataAttribute<TestDataModel>(typeof(TestDataModel), "CsvBadTestData.csv");
		var methodInfo = typeof(LoadFromCsvDataAttributeUnitTests).GetMethod(nameof(LoadCsvBadTestDataWithAttribute))!;

		// Act
		var action = () => attribute.GetData(methodInfo).ToList();

		// Assert
		var exception = action.Should().Throw<Exception>();
		exception.Subject.ToArray()[0].Source.Should().Be("CsvHelper");
	}

	[Fact]
	public void LoadCsvMissingFileWithAttribute()
	{
		// Arrange
		var attribute = new LoadFromCsvDataAttribute<TestDataModel>(typeof(TestDataModel), "MissingFile.csv");
		var methodInfo = typeof(LoadFromCsvDataAttributeUnitTests).GetMethod(nameof(LoadCsvBadTestDataWithAttribute))!;

		// Act
		var action = () => attribute.GetData(methodInfo).ToList();

		// Assert
		var exception = action.Should().Throw<Exception>();
		var subject = exception.Subject.ToArray()[0];
		subject.Source.Should().Be("Nabs.Tests");
		subject.Message.Should().Be("No resource info items found.");
	}

	[Fact]
	public void SkipAttribute()
	{
		// Arrange
		var attribute = new LoadFromCsvDataAttribute<TestDataModel>(typeof(TestDataModel), "MissingFile.csv");
		var methodInfo = typeof(LoadFromCsvDataAttributeUnitTests).GetMethod(nameof(LoadCsvBadTestDataWithAttribute))!;

		// Act
		var action = () => attribute.GetData(methodInfo).ToList();

		// Assert
		var exception = action.Should().Throw<Exception>();
		var subject = exception.Subject.ToArray()[0];
		subject.Source.Should().Be("Nabs.Tests");
		subject.Message.Should().Be("No resource info items found.");
	}
}