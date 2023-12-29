namespace Nabs.Tests.Fixtures;

public class SimpleTestFixture(IMessageSink diagnosticMessageSink) 
	: TestFixtureBase(diagnosticMessageSink)
{
}