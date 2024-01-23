namespace Nabs.Persistence;

public interface ITenantableDbContext
{
	public IApplicationContext ApplicationContext { get; }
}
