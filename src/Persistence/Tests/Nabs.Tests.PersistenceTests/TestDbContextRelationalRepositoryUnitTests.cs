namespace Nabs.Tests.PersistenceTests;

[Collection("TestDbContextDataLoader")]
public class TestDbContextRelationalRepositoryUnitTests : TestBase
{
    private readonly DataLoaderFixture _dataLoaderFixture;
    private readonly IContextRepository<TestDbContext> _testRepository;

    public TestDbContextRelationalRepositoryUnitTests(
        ITestOutputHelper output,
        DataLoaderFixture dataLoaderFixture)
        : base(dataLoaderFixture, output)
    {
        _dataLoaderFixture = dataLoaderFixture;
        _testRepository = dataLoaderFixture.ServiceScope.ServiceProvider
            .GetRequiredService<IContextRepository<TestDbContext>>();
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