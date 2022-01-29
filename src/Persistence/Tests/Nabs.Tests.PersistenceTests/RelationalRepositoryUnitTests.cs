namespace Nabs.Tests.PersistenceTests;

[Collection("TestDbContextDataLoader")]
public class RelationalRepositoryUnitTests : TestBase
{
    private IRepository<TestDbContext> _testRepository;

    public RelationalRepositoryUnitTests(
        ITestOutputHelper output,
        DataLoaderFixture dataLoaderFixture)
        : base(dataLoaderFixture, output)
    {
        _testRepository = dataLoaderFixture.ServiceScope.ServiceProvider
            .GetRequiredService<IRepository<TestDbContext>>();
    }

    public override async Task StartTest()
    {
        await TestFixture.EnsureDatabaseLoaderAsync();
    }

    [Fact]
    public async Task GetItem_FirstTestUser_Success()
    {
        //Arrange

        //Act
        var actual = await _testRepository.GetItem<TestUser>().ExecuteAsync();

        //Assert
        actual.Should().NotBeNull();
    }

    [Fact]
    public async Task GetItem_SpecificTestUser_Success()
    {
        //Arrange
        var id = Guid.NewGuid();
        var newTestUser = new TestUser(id, $"un:{id}", $"fn:{id}");

        newTestUser = await _testRepository
            .ItemCommand<TestUser>()
            .ForItem(newTestUser)
            .ExecuteAsync();

        //Act
        var actual = await _testRepository
            .GetItem<TestUser>()
            .WithPredicate(_ => _.Id == id)
            .ExecuteAsync();

        //Assert
        actual.Should().NotBeNull();
    }
}