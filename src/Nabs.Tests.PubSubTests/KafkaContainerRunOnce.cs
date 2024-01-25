namespace Nabs.Tests.PubSubTests;

public sealed class KafkaContainerRunOnce : XunitTestFramework, IDisposable
{
	public const string RunOnceFqn = "Nabs.Tests.PubSubTests.KafkaContainerRunOnce";
	public const string RunOnceAssemblyName = "Nabs.Tests.PubSubTests";

	private readonly KafkaContainer _container;

	public KafkaContainerRunOnce(IMessageSink messageSink)
		: base(messageSink)
	{
		DiagnosticMessageSink.OnMessage(new DiagnosticMessage("Kafka Container starting ..."));

		_container = new KafkaBuilder()
			.WithImage("confluentinc/cp-kafka:6.2.10")
			.Build();
		
		_container.StartAsync().GetAwaiter().GetResult();
	}

	public new void Dispose()
	{
		DiagnosticMessageSink.OnMessage(new DiagnosticMessage("Kafka Container stopping ..."));
		_container.StopAsync().GetAwaiter().GetResult();
		GC.SuppressFinalize(this);
		base.Dispose();
		DiagnosticMessageSink.OnMessage(new DiagnosticMessage("Kafka Container stopped!"));
	}
}
