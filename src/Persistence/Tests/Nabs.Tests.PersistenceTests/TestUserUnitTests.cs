namespace Nabs.Tests.PersistenceTests;

[Collection(nameof(TestDbContextDataLoaderFixtureCollection))]
public class TestUserUnitTests : TestBase
{
    private readonly DataLoaderFixture _dataLoaderFixture;
    private readonly IRelationalRepository<TestDbContext> _testRepository;
    private readonly IQueryItem<TestUser> _testUserQuery;
    
    private Guid _id;
    private TestUser _newTestUser;

    public TestUserUnitTests(
        ITestOutputHelper output,
        DataLoaderFixture dataLoaderFixture)
        : base(dataLoaderFixture, output)
    {
        _dataLoaderFixture = dataLoaderFixture;
        _dataLoaderFixture.Should().NotBeNull();

        _testRepository = dataLoaderFixture.ServiceScope.ServiceProvider
            .GetRequiredService<IRelationalRepository<TestDbContext>>();
        _testRepository.Should().NotBeNull();

        _testUserQuery = _testRepository.QueryItem<TestUser>();
        _testUserQuery.Should().NotBeNull();
    }

    public override async Task StartTest()
    {
        //Arrange
        await TestFixture.EnsureDatabaseLoaderAsync();

        _id = Guid.NewGuid();
        _newTestUser = await _dataLoaderFixture
            .TestDbContextDataLoader!
            .CreateTestUser(_id);
    }
    
    [Fact]
    public async Task GetItem_FirstTestUser_Success()
    {
        //Act
        var actual = await _testUserQuery
            .ExecuteAsync();

        //Assert
        actual.Should().NotBeNull();
    }


    [Fact]
    public async Task QueryItem_TestUserById_Success()
    {
        //Act
        var actual = await _testUserQuery
            .WithId(_id)
            .ExecuteAsync();

        //Assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(_newTestUser);
    }

    [Fact]
    public async Task QueryItem_SpecificTestUser_Success()
    {
        //Act
        var actual = await _testUserQuery
            .WithPredicate(_ => _.Id == _id)
            .ExecuteAsync();

        //Assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(_newTestUser);
    }

    [Fact]
    public async Task UpdateAndQueryItem_SpecificTestUser_Success()
    {
        //Arrange
        var (id, username, firstName) = _newTestUser;
        var itemToUpdate = new TestUser(id, username, "fn: updated first name");
        var updatedItem = await _testRepository
            .ItemCommand<TestUser>()
            .ForItem(itemToUpdate)
            .ExecuteAsync();
        
        //Act
        var actual = await _testUserQuery
            .WithPredicate(_ => _.Id == _id)
            .ExecuteAsync();

        //Assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(updatedItem);
        updatedItem.Should().NotBeEquivalentTo(_newTestUser);
    }
}