using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;
using Xunit.Sdk;

[assembly: Xunit.TestFramework("Nabs.Tests.TestDatabaseTests.RunOnce", "Nabs.Tests.TestDatabaseTests")]

namespace Nabs.Tests.TestDatabaseTests;

public class RunOnce : XunitTestFramework, IDisposable
{
	private readonly MsSqlContainer _container;
	public RunOnce(IMessageSink messageSink)
		: base(messageSink)
	{
		_container = new MsSqlBuilder()
			.WithName("nabs-test-tenantable-db")
			.WithPortBinding(14331, 1433)
			.WithPassword("Password123")
			.Build();

		_container.StartAsync().GetAwaiter().GetResult();
	}

	public new void Dispose()
	{
		_container.StopAsync().GetAwaiter().GetResult();
		base.Dispose();
	}
}