using System.Diagnostics.CodeAnalysis;

namespace Nabs.Tests.TestsTests.Playbooks.SimpleTestWithOutputLogging;

[Collection(nameof(SimpleFixtureCollection))]
[ExcludeFromCodeCoverage]
public class SimpleTestWithOutputLoggingUnitTests(ITestOutputHelper testOutputHelper, SimpleTestFixture fixture)
		: FixtureTestBase<SimpleTestFixture>(testOutputHelper, fixture)
{
	[Fact]
	public void OutputInitialised__Success()
	{
		OutputScenario();

		//Assert
	}


	[Fact]
	public void Outputs__Success()
	{
		OutputScenario();
		OutputStep("#1");
		OutputStep("#2");

		//Assert
		//Manually view the Test Details Summary output window for results.
	}

	[Theory]
	[InlineData("A", 1, 2, 2)]
	[InlineData("B", 1, 1, 1)]
	[InlineData("C", 1, 0, 0)]
	public void Multiplication__Success(string scenario, int a, int b, int expected)
	{
		//Arrange
		OutputScenario(scenario);
		
		//Act
		var actual = a * b;

		//Assert
		actual.Should().Be(expected);
	}

	[Fact]
	public void Multiply_A_and_B__Success()
	{
		//Arrange
		const int a = 2;
		const int b = 2;
		const int expected = 4;
		
		//Act
		const int actual = a * b;

		//Assert
		actual.Should().Be(expected);
	}

	[Fact]
	public void Fact_Test_Method__Success()
	{
		//Arrange
		
		//Act

		//Assert
	}

	[Theory]
	[InlineData("A")]
	public void TheoryTestMethod__Success(string scenario)
	{
		//Arrange
		OutputScenario(scenario);

		//Act

		//Assert
	}
}