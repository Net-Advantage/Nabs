using Nabs.Tests.TestsUnitTests.Runbooks.TestWithConfiguration;

namespace Nabs.Tests.TestsUnitTests;

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