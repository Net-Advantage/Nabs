namespace Nabs.Tests.TestDatabaseUnitTests;

public class SimpleUnsetTenantDatabaseUnitTests(
	ITestOutputHelper testOutputHelper,
	SimpleUnsetTenantDatabaseTestFixture testFixture)
	: DatabaseTestBase<SimpleUnsetTenantDatabaseTestFixture>(testOutputHelper, testFixture)
{
	private SimpleTenantDbContext _dbContext = default!;

	protected override async Task StartTest()
	{
		var dbContextFactory = TestFixture.ServiceScope.ServiceProvider
			.GetRequiredService<IDbContextFactory<SimpleTenantDbContext>>();
		_dbContext = dbContextFactory.CreateDbContext();
		await _dbContext.Database.EnsureDeletedAsync();
		await _dbContext.Database.EnsureCreatedAsync();
	}

	[Fact]
	public void RunTest()
	{
		// Arrange
		var comment = new CommentEntity()
		{
			Id = Guid.NewGuid(),
			Comments = "This is the comment!"
		};

		// Act
		_dbContext.Comments.Add(comment);
		var action = () => _dbContext.SaveChanges();

		// Assert
		action.Should().Throw<InvalidOperationException>();
	}
}


public class SimpleTenantDatabaseUnitTests(
ITestOutputHelper testOutputHelper,
SimpleTenantDatabaseTestFixture testFixture)
: DatabaseTestBase<SimpleTenantDatabaseTestFixture>(testOutputHelper, testFixture)
{
	private SimpleTenantDbContext _dbContext = default!;

	protected override async Task StartTest()
	{
		var dbContextFactory = TestFixture.ServiceScope.ServiceProvider
			.GetRequiredService<IDbContextFactory<SimpleTenantDbContext>>();
		_dbContext = dbContextFactory.CreateDbContext();
		await _dbContext.Database.EnsureDeletedAsync();
		await _dbContext.Database.EnsureCreatedAsync();
	}

	[Theory]
	[LoadEnumerableFromJsonData<SimpleTenantEntity>(typeof(SimpleTenantDatabaseUnitTests), ".SimpleTenantEntityItems.json")]
	public async Task AddTenant_Success(SimpleTenantEntity item)
	{
		// Arrange
		item.Id = _dbContext.ApplicationContext.TenantContext.TenantId;

		// Act
		_dbContext.Tenants.Add(item);
		_dbContext.SaveChanges();

		_dbContext.ChangeTracker.Clear();
		var result = await _dbContext.Tenants
			.AsNoTracking()
			.ToListAsync();

		// Assert
		result.Should().NotBeEmpty();
		result.Count.Should().Be(1);

		var json = DefaultJsonSerializer.Serialize(result);
		TestFixture.TestOutputHelper.WriteLine(json);
	}

	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	public async Task AddCommentsOverload1_Success(int overload)
	{
		// Arrange
		var comment = new CommentEntity()
		{
			Id = Guid.NewGuid(),
			Comments = "This is the comment!"
		};

		// Act
		_dbContext.Comments.Add(comment);

		if (overload == 1)
		{
			_dbContext.SaveChanges();
		}
		else if (overload == 2)
		{
			await _dbContext.SaveChangesAsync();
		}
		else
		{
			false.Should().BeTrue("Invalid overload specified");
		}

		_dbContext.ChangeTracker.Clear();
		var result = await _dbContext.Comments
			.AsNoTracking()
			.ToListAsync();

		// Assert
		result.Should().NotBeEmpty();
		result.Count.Should().Be(1);

		var json = DefaultJsonSerializer.Serialize(result);
		TestFixture.TestOutputHelper.WriteLine(json);
	}
}