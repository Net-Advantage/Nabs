
namespace Nabs.Tests.DatabaseTests;

public abstract class DatabaseTestBase<TDatabaseFixture>(
	ITestOutputHelper testOutputHelper, 
	TDatabaseFixture testFixture)
	: FixtureTestBase<TDatabaseFixture>(testOutputHelper, testFixture)
	where TDatabaseFixture : DatabaseFixtureBase, IDatabaseFixtureBase
{
}
