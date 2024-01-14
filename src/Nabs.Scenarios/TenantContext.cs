namespace Nabs.Scenarios;

public sealed class TenantContext : ITenantContext
{
	public Guid TenantId { get; init; }
}
