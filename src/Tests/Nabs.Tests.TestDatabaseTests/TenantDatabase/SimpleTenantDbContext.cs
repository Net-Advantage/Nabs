namespace Nabs.Tests.TestDatabaseTests.TenantDatabase;

public sealed class SimpleTenantDbContext(
	DbContextOptions options, 
	IApplicationContext applicationContext)
	: TenantableDbContext<SimpleTenantEntity>(options, applicationContext)
{
	public DbSet<CommentEntity> Comments => Set<CommentEntity>();
}
