namespace Nabs.Tests.PersistenceTests;

[Collection("TestDbContextDataLoader")]
public class RelationalRepositoryUnitTests : TestBase
{
    private readonly IRepository<TestDbContext> _testRepository;

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
        var actual = await _testRepository.QueryItem<TestUser>().ExecuteAsync();

        //Assert
        actual.Should().NotBeNull();
    }

    [Fact]
    public async Task GetItem_SpecificTestUser_Success()
    {
        //Arrange
        var id = Guid.NewGuid();
        var newTestUser = new TestUser(id, $"un:{id}", $"fn:{id}");
        var command = _testRepository.ItemCommand<TestUser>();
        var query = _testRepository.QueryItem<TestUser>();

        newTestUser = await command
            .ForItem(newTestUser)
            .ExecuteAsync();

        //Act
        var actual = await query
            .WithPredicate(_ => _.Id == id)
            .ExecuteAsync();

        //Assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(newTestUser);
    }
}