namespace Nabs.Tests.TestDatabaseTests;

public sealed class TenantIsolationStrategyUnitTest(
	ITestOutputHelper testOutputHelper,
	TenantIsolationStrategyTestFixture testFixture)
	: DatabaseTestBase<TenantIsolationStrategyTestFixture>(testOutputHelper, testFixture)
{
	private ITenantableDbContextFactory<SimpleTenantDbContext> _dbContextFactory = default!;

	protected override Task StartTest()
	{
		_dbContextFactory = TestFixture.ServiceScope.ServiceProvider.GetRequiredService<ITenantableDbContextFactory<SimpleTenantDbContext>>();
		return Task.CompletedTask;
	}

	private SimpleTenantDbContext EnsureAndGetDbContext()
	{
		var applicationContext = TestFixture.ServiceScope.ServiceProvider.GetRequiredService<IApplicationContext>();
		var dbContext = _dbContextFactory.CreateDbContext(applicationContext);
		dbContext.Database.EnsureCreated();
		return dbContext;
	}

	[Fact]
	public void DedicatedDedicatedRunTest()
	{
		var tenantId = new Guid("731724a1-9b57-46ce-baaf-7325bc8711c0");
		TestFixture.ApplicationContextFactory = () => new ApplicationContext()
		{
			TenantIsolationStrategy = TenantIsolationStrategy.DedicatedDedicated,
			TenantContext = new TenantContext()
			{
				TenantId = tenantId
			}
		};

		var dbContext = EnsureAndGetDbContext();

		// Act
		var connectionString = dbContext.Database.GetConnectionString()!;

		// Assert
		connectionString.Should().Contain($"Database=SimpleTenantDb_DedicatedDedicated_{tenantId};");
	}

	[Theory]
	[InlineData("731724a1-9b57-46ce-baaf-7325bc8711c0", "aa9b2c14-ff4e-46ed-8ebe-e7b96be2cdbf")]
	[InlineData("931d3b9a-4931-4577-bbe0-dc913db3d3c9", "ca3608e2-82e0-4f02-a522-d6904839033e")]
	public void SharedSharedRunTest(Guid tenantId, Guid userId)
	{
		_ = userId;

		TestFixture.ApplicationContextFactory = () => new ApplicationContext()
		{
			TenantIsolationStrategy = TenantIsolationStrategy.SharedShared,
			TenantContext = new TenantContext()
			{
				TenantId = tenantId
			}
		};

		var dbContext = EnsureAndGetDbContext();

		// Act
		var connectionString = dbContext.Database.GetConnectionString()!;

		// Assert
		connectionString.Should().Contain($"Database=SimpleTenantDb_SharedShared");
	}

	[Theory]
	[InlineData("3adcf84d-0e30-4307-92ee-874762fa74ca", "aa9b2c14-ff4e-46ed-8ebe-e7b96be2cdbf")]
	[InlineData("a41ad640-bb6b-49ed-a5f8-a79ddf8b2c69", "ca3608e2-82e0-4f02-a522-d6904839033e")]
	public void SharedDedicatedRunTest(Guid tenantId, Guid userId)
	{
		_ = userId;

		TestFixture.ApplicationContextFactory = () => new ApplicationContext()
		{
			TenantIsolationStrategy = TenantIsolationStrategy.SharedDedicated,
			TenantContext = new TenantContext()
			{
				TenantId = tenantId
			}
		};

		var dbContext = EnsureAndGetDbContext();

		// Act
		var connectionString = dbContext.Database.GetConnectionString()!;

		// Assert
		connectionString.Should().Contain($"Database=SimpleTenantDb_SharedDedicated_{tenantId}");
	}
}
