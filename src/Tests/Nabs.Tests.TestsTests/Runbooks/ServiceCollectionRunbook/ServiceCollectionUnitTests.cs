namespace Nabs.Tests.TestsTests.Runbooks.TestWithConfiguration;

[Collection(nameof(SimpleConfigurationFixtureCollection))]
[ExcludeFromCodeCoverage]
public class ServiceCollectionUnitTests(
	ITestOutputHelper testOutputHelper, SimpleConfigurationTestFixture fixture)
		: FixtureTestBase<SimpleConfigurationTestFixture>(testOutputHelper, fixture)
{


	[Fact]
	public void FixtureIsInitialised()
	{
		// Arrange
		// Act
		// Assert
		TestFixture.Should().NotBeNull();
		TestFixture.ConfigurationRoot.Should().NotBeNull();
		TestFixture.ServiceProvider.Should().NotBeNull();
		TestFixture.ServiceScope.Should().NotBeNull();
	}
}
