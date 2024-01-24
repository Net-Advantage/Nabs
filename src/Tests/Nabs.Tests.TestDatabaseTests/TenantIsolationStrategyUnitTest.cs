namespace Nabs.Tests.TestDatabaseTests;

public sealed class TenantIsolationStrategyUnitTest(
	ITestOutputHelper testOutputHelper,
	TenantIsolationStrategyTestFixture testFixture)
	: DatabaseTestBase<TenantIsolationStrategyTestFixture>(testOutputHelper, testFixture)
{
	[Theory]
	[InlineData("731724a1-9b57-46ce-baaf-7325bc8711c0", TenantIsolationStrategy.DedicatedDedicated)]
	[InlineData("931d3b9a-4931-4577-bbe0-dc913db3d3c9", TenantIsolationStrategy.SharedShared)]
	[InlineData("3adcf84d-0e30-4307-92ee-874762fa74ca", TenantIsolationStrategy.SharedShared)]
	[InlineData("a41ad640-bb6b-49ed-a5f8-a79ddf8b2c69", TenantIsolationStrategy.SharedShared)]
	[InlineData("ca3608e2-82e0-4f02-a522-d6904839033e", TenantIsolationStrategy.SharedDedicated)]
	[InlineData("b36c1502-7dca-4320-8ea2-f9cd486da07e", TenantIsolationStrategy.SharedDedicated)]
	public void SharedSharedRunTest(Guid tenantId, TenantIsolationStrategy tenantIsolationStrategy)
	{
		TestFixture.ApplicationContextFactory = () => new ApplicationContext()
		{
			TenantIsolationStrategy = tenantIsolationStrategy,
			TenantContext = new TenantContext()
			{
				TenantId = tenantId
			}
		};

		var dbContextFactory = TestFixture.ServiceScope.ServiceProvider.GetRequiredService<ITenantableDbContextFactory<SimpleTenantDbContext>>();
		var applicationContext = TestFixture.ServiceScope.ServiceProvider.GetRequiredService<IApplicationContext>();
		var dbContext = dbContextFactory.CreateDbContext(applicationContext);
		dbContext.Database.EnsureCreated();

		// Act
		var connectionString = dbContext.Database.GetConnectionString()!;

		// Assert
		if (tenantIsolationStrategy == TenantIsolationStrategy.SharedShared)
		{
			connectionString.Should().Contain($"Database=SimpleTenantDb_SharedShared");
			connectionString.Should().NotContain($"{tenantId}");
		}
		else
		{
			connectionString.Should().Contain($"Database=SimpleTenantDb_{tenantIsolationStrategy}");
			connectionString.Should().Contain($"{tenantId}");
		}

		dbContext.ApplicationContext.TenantContext.TenantId.Should().Be(tenantId);
	}
}