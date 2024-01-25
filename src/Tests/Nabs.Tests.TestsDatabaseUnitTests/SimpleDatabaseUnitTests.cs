namespace Nabs.Tests.TestDatabaseUnitTests;

public class SimpleDatabaseUnitTests(
	ITestOutputHelper testOutputHelper,
	SimpleDatabaseTestFixture testFixture)
	: DatabaseTestBase<SimpleDatabaseTestFixture>(testOutputHelper, testFixture)
{
	private SimpleDbContext _dbContext = default!;

	protected override async Task StartTest()
	{
		var dbContextFactory = TestFixture.ServiceScope.ServiceProvider.GetRequiredService<IDbContextFactory<SimpleDbContext>>();
		_dbContext = dbContextFactory.CreateDbContext();
		await _dbContext.Database.EnsureDeletedAsync();
		await _dbContext.Database.EnsureCreatedAsync();
	}

	[Theory]
	[LoadEnumerableFromJsonData<AllTypesEntity>(typeof(SimpleDatabaseUnitTests), ".AllTypesEntityItems.json")]
	public async Task RunTests(AllTypesEntity item)
	{
		// Arrange

		// Act
		_dbContext.AllTypesEntities.Add(item);
		_dbContext.SaveChanges();

		_dbContext.ChangeTracker.Clear();
		var result = await _dbContext.AllTypesEntities
			.AsNoTracking()
			.ToListAsync();

		// Assert
		result.Should().NotBeEmpty();
		result.Count.Should().Be(1);

		var json = DefaultJsonSerializer.Serialize(result);
		TestFixture.TestOutputHelper.WriteLine(json);
	}
}
