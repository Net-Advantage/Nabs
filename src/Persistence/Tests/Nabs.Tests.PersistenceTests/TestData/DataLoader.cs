namespace Nabs.Tests.PersistenceTests.TestData;

public class DataLoader
{
    private bool _hasRun = false;

    public async Task LoadAsync(IRepository<TestDbContext> repository)
    {
        if (_hasRun)
        {
            return;
        }

        var context = repository.NewDbContext();
        await context.Database.EnsureCreatedAsync();
        
        var id = Guid.NewGuid();
        var testUser = new TestUser(id, $"un:{id.ToString()}", $"fn:{id.ToString()}");

        var result = repository
            .ItemCommand<TestUser>()
            .ForItem(testUser)
            .ExecuteAsync();

        _hasRun = true;
    }
}