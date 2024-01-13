namespace Nabs.Tests.AzureConfigurationTests;

[Collection(nameof(AzureConfigurationTestFixtureCollection))]
public class SetupUnitTest(
	ITestOutputHelper testOutputHelper,
	AzureConfigurationTestFixture testFixture) 
	: FixtureTestBase<AzureConfigurationTestFixture>(testOutputHelper, testFixture)
{
	[Fact]
	public void Test1()
	{
	}
}