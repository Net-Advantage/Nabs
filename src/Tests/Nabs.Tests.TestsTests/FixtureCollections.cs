using Nabs.Tests.TestsTests.Runbooks.TestWithConfiguration;

namespace Nabs.Tests.TestsTests;

[CollectionDefinition(nameof(SimpleTestFixtureCollection))]
public sealed class SimpleTestFixtureCollection
	: ICollectionFixture<SimpleTestFixture>
{

}

[CollectionDefinition(nameof(SimpleConfigurationFixtureCollection))]
public sealed class SimpleConfigurationFixtureCollection
	: ICollectionFixture<SimpleConfigurationTestFixture>
{

}