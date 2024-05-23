namespace Nabs.Tests.PubSubTestsUnitTests;

public class SimpleKafkaPubSubFixture(
    IMessageSink diagnosticMessageSink)
    : KafkaPubSubFixtureBase(diagnosticMessageSink)
{
    public IProducer<string, string> KafkaProducer { get; private set; } = default!;
    public IConsumer<string, string> KafkaConsumer { get; private set; } = default!;

    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        //Admin
        var kafkaAdminConfig = new AdminClientConfig
        {
            BootstrapServers = "localhost:9092"
        };
        using var adminClient = new AdminClientBuilder(kafkaAdminConfig)
            .Build();
        var topicName = "test-topic";
        adminClient.CreateTopicsAsync(new[]
        {
            new TopicSpecification
            {
                Name = topicName,
                NumPartitions = 1,
                ReplicationFactor = 1
            }
        }).GetAwaiter().GetResult();

        //Producer
        var kafkaProducerConfig = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            ClientId = "nabs-test",
            SecurityProtocol = SecurityProtocol.Plaintext,
            Acks = Acks.All,
            CompressionType = CompressionType.None,
            AllowAutoCreateTopics = true
        };

        KafkaProducer = new ProducerBuilder<string, string>(kafkaProducerConfig)
            .Build();

        //Consumer
        var kafkaConsumerConfig = new ConsumerConfig()
        {
            BootstrapServers = "localhost:9092",
            ClientId = "nabs-test",
            SecurityProtocol = SecurityProtocol.Plaintext,
            Acks = Acks.All,
            GroupId = "nabs-test-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            AllowAutoCreateTopics = true
        };

        KafkaConsumer = new ConsumerBuilder<string, string>(kafkaConsumerConfig)
            .Build();

    }
}