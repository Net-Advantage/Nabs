namespace Nabs.Tests.PersistenceTests.DbContexts;

public class TestDbContext : DbContext
{
	public TestDbContext(DbContextOptions<TestDbContext> dbContextOptions)
		: base(dbContextOptions)
	{
	}

	public DbSet<TestUser> Users { get; set; }
}