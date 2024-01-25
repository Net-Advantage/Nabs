using Nabs.Scenarios;
using Nabs.Tests.Fixtures;

namespace Nabs.Tests.PubSubTests;

public interface IKafkaPubSubFixtureBase : ITestFixture
{
	Func<IApplicationContext>? ApplicationContextFactory { get; set; }
};

public abstract class KafkaPubSubFixtureBase(
	IMessageSink diagnosticMessageSink)
	: ConfigurationTestFixtureBase(diagnosticMessageSink), IKafkaPubSubFixtureBase
{
	public Func<IApplicationContext>? ApplicationContextFactory { get; set; }
}