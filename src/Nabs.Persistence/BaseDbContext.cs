namespace Nabs.Persistence;

public abstract class BaseDbContext(DbContextOptions options)
	: DbContext(options), ITenantableDbContext
{
	public Guid TenantId { get; set; }
}