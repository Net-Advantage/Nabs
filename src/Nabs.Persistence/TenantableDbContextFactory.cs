namespace Nabs.Persistence;

public interface ITenantableDbContextFactory<TDbContext> : IDbContextFactory<TDbContext>
	where TDbContext : DbContext, ITenantableDbContext
{
	TDbContext CreateDbContext(IApplicationContext applicationContext);

	[Obsolete("Use the CreateDbContext method that requires an IApplicationContext.")]
	new TDbContext CreateDbContext();
}

public class TenantableDbContextFactory<TDbContext>(
	string databaseNamePrefix,
	IConfigurationRoot configurationRoot)
	: ITenantableDbContextFactory<TDbContext>
	where TDbContext : DbContext, ITenantableDbContext
{
	//TODO: DWS: This needs to be set up properly
	const string _testConnectionStringTemplate = "Server=localhost,14331;Database={0};User Id=sa;Password=Password123;TrustServerCertificate=True;";

	//TODO: DWS: This assumes azure Sql we will need to provide various options for clouds other than Azure.
	const string _envConnectionStringTemplate = "Server=tcp:{0};Database={1};Authentication=Active Directory Default;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


	private readonly string _databaseNamePrefix = databaseNamePrefix;
	private readonly IConfigurationRoot _configurationRoot = configurationRoot;

	public TDbContext CreateDbContext(IApplicationContext applicationContext)
	{
		var databaseName = $"{_databaseNamePrefix}_";
		if (applicationContext.TenantIsolationStrategy == TenantIsolationStrategy.SharedShared)
		{
			databaseName += "SharedShared";
		}
		else
		{
			databaseName += $"{applicationContext.TenantIsolationStrategy}_{applicationContext.TenantContext.TenantId}";
		}

		var serverName = _configurationRoot.GetConnectionString($"{_databaseNamePrefix}ServerName");
		string? connectionString;
		if (string.IsNullOrWhiteSpace(serverName))
		{
			connectionString = string.Format(_testConnectionStringTemplate, databaseName);
		}
		else
		{
			connectionString = string.Format(_envConnectionStringTemplate, serverName, databaseName);
		}

		var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
		optionsBuilder.UseSqlServer(connectionString);

		var options = optionsBuilder
			.Options;

		return (TDbContext)Activator.CreateInstance(typeof(TDbContext), options, applicationContext)!;
	}

	[Obsolete("Use the CreateDbContext method that requires an IApplicationContext.")]
	public TDbContext CreateDbContext()
	{
		throw new NotImplementedException();
	}
}
