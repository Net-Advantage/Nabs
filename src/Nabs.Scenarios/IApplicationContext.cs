namespace Nabs.Scenarios;


public interface IApplicationContext
{
    TenantIsolationStrategy TenantIsolationStrategy { get; init; }

    ITenantContext TenantContext { get; init; }
    IUserContext UserContext { get; init; }

    public bool IsTenant(Guid tenantId);
}
