namespace Nabs;

public interface ITenantEntity
{
	Guid Id { get; set; }
	string Name { get; set; }
	TenantIsolationStrategy IsolationStrategy { get; set; }
}
