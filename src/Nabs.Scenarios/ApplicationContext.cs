namespace Nabs.Scenarios;

public sealed class ApplicationContext : IApplicationContext
{
	public ApplicationContext()
	{
		TenantContext = new TenantContext();
		UserContext = new UserContext();
	}

	public TenantIsolationStrategy TenantIsolationStrategy { get; init; }
	public ITenantContext TenantContext { get; init; }
	public IUserContext UserContext { get; init; }

	public bool IsTenant(Guid tenantId)
	{
		return TenantContext.TenantId == tenantId;
	}
}
