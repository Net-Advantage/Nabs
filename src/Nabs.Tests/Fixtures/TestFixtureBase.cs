namespace Nabs.Tests.Fixtures;

public abstract class TestFixtureBase : IDisposable
{
	private readonly IMessageSink _diagnosticMessageSink;

	protected TestFixtureBase(IMessageSink diagnosticMessageSink)
	{
		_diagnosticMessageSink = diagnosticMessageSink;
	}

	public ITestOutputHelper TestOutputHelper {get; set; }

	public void OutputLine(string message)
	{
		_diagnosticMessageSink.OnMessage(new DiagnosticMessage(message));
		TestOutputHelper?.WriteLine(message);
	}

	public void Dispose()
	{
		//See: https://learn.microsoft.com/en-gb/dotnet/fundamentals/code-analysis/quality-rules/ca1816
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{

	}
}