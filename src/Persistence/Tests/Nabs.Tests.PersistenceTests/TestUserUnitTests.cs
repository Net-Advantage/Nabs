namespace Nabs.Tests.PersistenceTests;

[Collection("TestDbContextDataLoader")]
public class TestUserUnitTests : TestBase
{
    private readonly DataLoaderFixture _dataLoaderFixture;
    private readonly IContextRepository<TestDbContext> _testRepository;
    private readonly IQueryItem<TestUser> _testUserQuery;

    public TestUserUnitTests(
        ITestOutputHelper output,
        DataLoaderFixture dataLoaderFixture)
        : base(dataLoaderFixture, output)
    {
        _dataLoaderFixture = dataLoaderFixture;
        _dataLoaderFixture.Should().NotBeNull();

        _testRepository = dataLoaderFixture.ServiceScope.ServiceProvider
            .GetRequiredService<IContextRepository<TestDbContext>>();
        _testRepository.Should().NotBeNull();

        _testUserQuery = _testRepository.QueryItem<TestUser>();
        _testUserQuery.Should().NotBeNull();
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
        var actual = await _testUserQuery.ExecuteAsync();

        //Assert
        actual.Should().NotBeNull();
    }


    [Fact]
    public async Task GetItem_TestUserById_Success()
    {
        //Arrange
        var id = Guid.NewGuid();

        var newTestUser = await _dataLoaderFixture
            .TestDbContextDataLoader!
            .CreateTestUser(id);

        //Act
        var actual = await _testUserQuery
            .WithId(id)
            .ExecuteAsync();

        //Assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(newTestUser);
    }

    [Fact]
    public async Task GetItem_SpecificTestUser_Success()
    {
        //Arrange
        var id = Guid.NewGuid();

        var newTestUser = await _dataLoaderFixture
            .TestDbContextDataLoader!
            .CreateTestUser(id);
        
        //Act
        var actual = await _testUserQuery
            .WithPredicate(_ => _.Id == id)
            .ExecuteAsync();

        //Assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(newTestUser);
    }
}