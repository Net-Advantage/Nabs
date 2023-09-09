namespace Nabs.Tests.PersistenceTests.DbContexts;

public class TestDbContext : DbContext, ITestDbContext
{
	public TestDbContext()
	{
		
	}

	public TestDbContext(DbContextOptions<TestDbContext> dbContextOptions)
		: base(dbContextOptions)
	{
	}

	public virtual DbSet<TestUser> Users { get; set; } = default!;
}