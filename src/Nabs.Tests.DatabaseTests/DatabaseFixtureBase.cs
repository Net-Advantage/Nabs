namespace Nabs.Tests.DatabaseTests;

public interface IDatabaseFixtureBase : ITestFixture
{
	Func<IApplicationContext>? ApplicationContextFactory { get; set; }
};

public abstract class DatabaseFixtureBase(
	IMessageSink diagnosticMessageSink)
	: ConfigurationTestFixtureBase(diagnosticMessageSink), IDatabaseFixtureBase
{
	public Func<IApplicationContext>? ApplicationContextFactory { get; set; }
}