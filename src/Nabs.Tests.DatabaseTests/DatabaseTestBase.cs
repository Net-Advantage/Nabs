
namespace Nabs.Tests.DatabaseTests;

public abstract class DatabaseTestBase<TDatabaseFixture>
	: FixtureTestBase<TDatabaseFixture>
	where TDatabaseFixture : DatabaseFixtureBase, IDatabaseFixtureBase
{
	protected DatabaseTestBase(ITestOutputHelper testOutputHelper, TDatabaseFixture testFixture) 
		: base(testOutputHelper, testFixture)
	{
		
	}
}
