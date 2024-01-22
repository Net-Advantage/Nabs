namespace Nabs.Tests.TestDatabaseTests;

public sealed class DedicatedTenantIsolationStrategyTestFixture(
	IMessageSink diagnosticMessageSink)
		: DatabaseFixtureBase(diagnosticMessageSink)
{
	protected override void ConfigureServices(IServiceCollection services)
	{
		services.TryAddTransient<IApplicationContext>((sp) =>
			ApplicationContextFactory?.Invoke() ?? new ApplicationContext()
			{
				TenantContext = new TenantContext()
				{
					TenantId = Guid.Empty
				}
			});

		services.AddPersistence<SimpleTenantDbContext>("SimpleDbContext", ConfigurationRoot);
	}
}

public sealed class DedicatedTenantIsolationStrategyUnitTest(
	ITestOutputHelper testOutputHelper,
	DedicatedTenantIsolationStrategyTestFixture testFixture)
	: DatabaseTestBase<DedicatedTenantIsolationStrategyTestFixture>(testOutputHelper, testFixture)
{
	[Fact]
	public async Task DedicatedRunTest()
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

		var dbContextFactory = TestFixture.ServiceScope.ServiceProvider.GetRequiredService<IDbContextFactory<SimpleTenantDbContext>>();
		var dbContext = dbContextFactory.CreateDbContext();

		// Act
		var tenantComments = await dbContext.Comments
			.AsNoTracking()
			.ToListAsync();

		// Assert
		Assert.Single(tenantComments);

		var connectionString = dbContext.Database.GetConnectionString()!;
		TestFixture.TestOutputHelper.WriteLine($"ConnectionString: {connectionString}");
		Assert.Contains($"Database=TechTrekDb_DedicatedDedicated_{tenantId};", connectionString);
	}

	[Theory]
	[InlineData("731724a1-9b57-46ce-baaf-7325bc8711c0", "aa9b2c14-ff4e-46ed-8ebe-e7b96be2cdbf")]
	[InlineData("931d3b9a-4931-4577-bbe0-dc913db3d3c9", "ca3608e2-82e0-4f02-a522-d6904839033e")]
	public async Task SharedRunTest(Guid tenantId, Guid userId)
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

		var dbContextFactory = TestFixture.ServiceScope.ServiceProvider.GetRequiredService<IDbContextFactory<SimpleTenantDbContext>>();
		var dbContext = dbContextFactory.CreateDbContext();

		// Act
		var tenantComments = await dbContext.Comments
			.AsNoTracking()
			.ToListAsync();

		// Assert
		Assert.Single(tenantComments);

		var connectionString = dbContext.Database.GetConnectionString()!;
		TestFixture.TestOutputHelper.WriteLine($"ConnectionString: {connectionString}");
		Assert.Contains($"Database=TechTrekDb_SharedShared", connectionString);
	}
}
