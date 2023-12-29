namespace Nabs.Tests.PersistenceTests;

[Collection(nameof(TestDbContextDataLoaderFixtureCollection))]
public class TestDbContextRelationalRepositoryUnitTests(
	ITestOutputHelper testOutputHelper, 
	DataLoaderFixture dataLoaderFixture) 
	: TestBase<DataLoaderFixture>(testOutputHelper, dataLoaderFixture)
{
	private readonly IRelationalRepository<TestDbContext> _testRepository = dataLoaderFixture
		.ServiceProvider
		.GetRequiredService<IRelationalRepository<TestDbContext>>();


	[Fact]
	public void NewDbContext_Success()
	{
		//Arrange

		//Act
		var actual = _testRepository.NewDbContext();
		var users = actual.Users.ToList();

		//Assert
		actual.Should().NotBeNull();
		users.Should().NotBeNullOrEmpty();
	}
}