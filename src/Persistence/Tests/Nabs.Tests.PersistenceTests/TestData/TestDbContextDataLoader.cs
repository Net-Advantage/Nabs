namespace Nabs.Tests.PersistenceTests.TestData;

public class TestDbContextDataLoader
{
    private bool _hasRun = false;
    private readonly IRelationalRepository<TestDbContext> _repository;

    public TestDbContextDataLoader(IRelationalRepository<TestDbContext> repository)
    {

        _repository = repository;
    }

    public async Task LoadAsync()
    {
        if (_hasRun)
        {
            throw new InvalidOperationException("You cannot load the database more than once. Try reset first to clear all the data.");
        }

        var context = _repository.NewDbContext();
        await context.Database.EnsureCreatedAsync();

        var id = Guid.NewGuid();
        await CreateTestUser(id);

        _hasRun = true;
    }

    public async Task ResetAsync()
    {
        var context = _repository.NewDbContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }

    public async Task<TestUser> CreateTestUser(Guid id)
    {
        var testUser = new TestUser(id, $"un:{id}", $"fn:{id}", $"ln:{id}");

        var result = await _repository
            .ItemCommand<TestUser>()
            .ForItem(testUser)
            .ExecuteAsync();

        return result;
    }
}