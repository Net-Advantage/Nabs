namespace Nabs.Tests.DatabaseTests;

public sealed class DatabaseContainerRunOnce : XunitTestFramework, IDisposable
{
	public const string RunOnceFqn = "Nabs.Tests.DatabaseTests.DatabaseContainerRunOnce";
	public const string RunOnceAssemblyName = "Nabs.Tests.DatabaseTests";

	private readonly MsSqlContainer _container;

	public DatabaseContainerRunOnce(IMessageSink messageSink)
		: base(messageSink)
	{
		DiagnosticMessageSink.OnMessage(new DiagnosticMessage("Container starting ..."));

		_container = new MsSqlBuilder()
			.WithImage("mcr.microsoft.com/mssql/server:2019-latest")
			.WithName("nabs-test-mssql-db")
			.WithPortBinding(14331, 1433)
			.WithPassword("Password123")
			.WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
			.Build();

		StartContainerAndEnsureReady().GetAwaiter().GetResult();

	}

	public new void Dispose()
	{
		DiagnosticMessageSink.OnMessage(new DiagnosticMessage("Container stopping ..."));
		_container.StopAsync().GetAwaiter().GetResult();
		GC.SuppressFinalize(this);
		base.Dispose();
		DiagnosticMessageSink.OnMessage(new DiagnosticMessage("Container stopped!"));
	}

	private async Task StartContainerAndEnsureReady()
	{
		await _container.StartAsync();

		var retryCount = 1;
		var maxRetries = 5;
		var delay = TimeSpan.FromSeconds(10);
		var isReady = false;

		while (retryCount <= maxRetries && !isReady)
		{
			try
			{
				using var connection = new SqlConnection(_container.GetConnectionString());
				await connection.OpenAsync();
				isReady = true;
			}
			catch
			{
				DiagnosticMessageSink.OnMessage(new DiagnosticMessage($"Connection failed on attempt {retryCount}. Waiting for {delay.Seconds} seconds to try again ..."));
				// If connection fails, wait for a while and then retry
				await Task.Delay(delay);
				retryCount++;
			}
		}

		if (!isReady)
		{
			var failureMessage = $"Failed to establish a connection to the SQL Server container after {maxRetries} retries.";
			DiagnosticMessageSink.OnMessage(new DiagnosticMessage(failureMessage));
			throw new InvalidOperationException(failureMessage);
		}

		DiagnosticMessageSink.OnMessage(new DiagnosticMessage("Container is ready!"));
	}
}
