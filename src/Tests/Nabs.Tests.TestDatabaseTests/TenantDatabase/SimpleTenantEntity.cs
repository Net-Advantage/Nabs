namespace Nabs.Tests.TestDatabaseTests.TenantDatabase;

public sealed class SimpleTenantEntity : EntityBase<Guid>, ITenantEntity
{
	public string Name { get; set; } = string.Empty;
}