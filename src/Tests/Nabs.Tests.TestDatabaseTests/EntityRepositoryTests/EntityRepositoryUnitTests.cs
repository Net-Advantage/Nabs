using Microsoft.EntityFrameworkCore.Internal;

namespace Nabs.Tests.TestDatabaseTests.EntityRepositoryTests;

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

		var item = new AllTypesEntity()
		{
			StringColumn = "First",
		};
		dbContext.AllTypesEntities.Add(item);
		await dbContext.SaveChangesAsync();
	}

	[Fact]
	public async Task GetFirstAsync_WithSpecification_ReturnsFirstEntity()
	{
		// Arrange
		SimpleDbContext dbContext = _dbContextFactory.CreateDbContext();
		var repository = new EntityRepository<AllTypesEntity>(dbContext);
		var specification = new GetFirstAllTypesDtoSpecification();

		// Act
		var result = await repository.FirstOrDefaultAsync(specification);

		// Assert
		Assert.NotNull(result);
		Assert.Equal("First", result.StringColumn);
	}
}
