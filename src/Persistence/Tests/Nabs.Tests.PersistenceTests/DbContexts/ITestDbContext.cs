namespace Nabs.Tests.PersistenceTests.DbContexts;

public interface ITestDbContext
{
	DbSet<TestUser> Users { get; set; }
}