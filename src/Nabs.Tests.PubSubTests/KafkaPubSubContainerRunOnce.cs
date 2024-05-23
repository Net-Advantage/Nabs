namespace Nabs.Tests.PubSubTests;

public sealed class KafkaPubSubContainerRunOnce : XunitTestFramework, IDisposable
{
    public const string RunOnceFqn = "Nabs.Tests.PubSubTests.KafkaPubSubContainerRunOnce";
    public const string RunOnceAssemblyName = "Nabs.Tests.PubSubTests";

    private readonly KafkaContainer _container;

    public KafkaPubSubContainerRunOnce(IMessageSink messageSink)
        : base(messageSink)
    {
        DiagnosticMessageSink.OnMessage(new DiagnosticMessage("Kafka Container starting ..."));

        _container = new KafkaBuilder()
            .WithImage("confluentinc/cp-kafka:6.2.10")
            .WithName("nabs-test-pubsub-kafka")
            .WithPortBinding(9092, 9092)
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
