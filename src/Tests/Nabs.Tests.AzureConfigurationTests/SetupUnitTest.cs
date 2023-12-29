namespace Nabs.Tests.AzureConfigurationTests;

[Collection(nameof(AzureConfigurationTestFixtureCollection))]
public class SetupUnitTest(
	ITestOutputHelper testOutputHelper,
	AzureConfigurationTestFixture testFixture) 
	: TestBase<AzureConfigurationTestFixture>(testOutputHelper, testFixture)
{
	[Fact]
	public void Test1()
	{
	}
}