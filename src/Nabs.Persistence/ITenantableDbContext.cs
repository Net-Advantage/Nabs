namespace Nabs.Persistence;

public interface ITenantableDbContext
{
	public Guid TenantId { get; }
}
