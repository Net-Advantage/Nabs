namespace Nabs.Tests.PersistenceTests;

[Collection(nameof(TestDbContextDataLoaderFixtureCollection))]
public class TestDbContextRelationalRepositoryUnitTests : TestBase
{
    private readonly DataLoaderFixture _dataLoaderFixture;
    private readonly IRelationalRepository<TestDbContext> _testRepository;

    public TestDbContextRelationalRepositoryUnitTests(
        ITestOutputHelper output,
        DataLoaderFixture dataLoaderFixture)
        : base(dataLoaderFixture, output)
    {
        _dataLoaderFixture = dataLoaderFixture;
        _testRepository = dataLoaderFixture.ServiceScope.ServiceProvider
            .GetRequiredService<IRelationalRepository<TestDbContext>>();
    }

    [Fact]
    public void NewDbContext_Success()
    {
        //Arrange

        //Act
        var actual = _testRepository.NewDbContext();

        //Assert
        actual.Should().NotBeNull();
    }
}