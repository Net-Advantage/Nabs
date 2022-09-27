namespace Nabs.Tests.AzureConfigurationTests;

[Collection(nameof(AzureConfigurationTestFixtureCollection))]
public class SetupUnitTest : TestBase<AzureConfigurationTestFixture>
{
	public SetupUnitTest(
		AzureConfigurationTestFixture testFixture,
		ITestOutputHelper output) 
		: base(testFixture, output)
	{
	}

	[Fact]
	public void Test1()
	{
	}

	
}