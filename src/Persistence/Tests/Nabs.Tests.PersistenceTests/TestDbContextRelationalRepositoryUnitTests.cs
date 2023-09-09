namespace Nabs.Tests.PersistenceTests;

[Collection(nameof(TestDbContextDataLoaderFixtureCollection))]
public class TestDbContextRelationalRepositoryUnitTests : TestBase<DataLoaderFixture>
{
	private readonly IRelationalRepository<TestDbContext> _testRepository;

	public TestDbContextRelationalRepositoryUnitTests(
		DataLoaderFixture dataLoaderFixture,
		ITestOutputHelper output)
		: base(dataLoaderFixture, output)
	{
		_testRepository = dataLoaderFixture.ServiceScope.ServiceProvider
			.GetRequiredService<IRelationalRepository<TestDbContext>>();
	}

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