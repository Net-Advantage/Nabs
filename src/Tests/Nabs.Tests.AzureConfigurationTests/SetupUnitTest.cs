namespace Nabs.Tests.AzureConfigurationTests;

[Collection(nameof(AzureConfigurationTestFixtureCollection))]
public class SetupUnitTest : TestBase<AzureConfigurationTestFixture>
{
	public SetupUnitTest(
		ITestOutputHelper testOutputHelper, 
		AzureConfigurationTestFixture testFixture) 
		: base(testOutputHelper, testFixture)
	{
	}

	[Fact]
	public void Test1()
	{
	}
}