namespace Nabs.Tests.DatabaseTests;

public interface IDatabaseFixtureBase : ITestFixture
{
	Func<IApplicationContext>? ApplicationContextFactory { get; set; }
};

public abstract class DatabaseFixtureBase 
	: ConfigurationTestFixtureBase, IDatabaseFixtureBase
{
	protected DatabaseFixtureBase(IMessageSink diagnosticMessageSink) 
		: base(diagnosticMessageSink)
	{
	}

	public Func<IApplicationContext>? ApplicationContextFactory { get; set; }
}