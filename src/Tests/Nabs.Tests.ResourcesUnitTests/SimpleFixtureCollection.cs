using Nabs.Tests.Fixtures;

namespace Nabs.Tests.ResourcesUnitTests;

[CollectionDefinition(nameof(SimpleFixtureCollection))]
public sealed class SimpleFixtureCollection
	: ICollectionFixture<SimpleTestFixture>
{

}