namespace Nabs.Tests.TestDatabaseUnitTests.EntityRepositoryTests;

public sealed class EntityRepositoryUnitTests(
	ITestOutputHelper testOutputHelper,
	SimpleDatabaseTestFixture testFixture)
	: DatabaseTestBase<SimpleDatabaseTestFixture>(testOutputHelper, testFixture)
{
	private IDbContextFactory<SimpleDbContext> _dbContextFactory = default!;

	protected override async Task StartTest()
	{
		_dbContextFactory = TestFixture.ServiceScope.ServiceProvider.GetRequiredService<IDbContextFactory<SimpleDbContext>>();
		var dbContext = _dbContextFactory.CreateDbContext();
		await dbContext.Database.EnsureDeletedAsync();
		await dbContext.Database.EnsureCreatedAsync();

		var items = new List<AllTypesEntity>()
		{
			new()
			{
				StringColumn = "First"
			},
			new()
			{
				StringColumn = "Second"
			},
			new()
			{
				StringColumn = "Third"
			},
		};
		dbContext.AllTypesEntities.AddRange(items);
		await dbContext.SaveChangesAsync();
	}

	[Theory]
	[InlineData("First")]
	[InlineData("Second")]
	[InlineData("Third")]
	public async Task GetAllTypesDto_WithSpecification_ReturnsFirstEntity(string stringColumnValue)
	{
		// Arrange
		var dbContext = _dbContextFactory.CreateDbContext();
		var repository = new EntityRepository<AllTypesEntity>(dbContext);
		var specification = new GetAllTypesDtoSpecification(stringColumnValue);

		// Act
		var result = await repository.FirstOrDefaultAsync(specification);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(stringColumnValue, result.StringColumn);
	}
}
