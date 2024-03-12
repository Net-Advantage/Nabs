namespace Nabs.Tests.PubSubTestsUnitTests;

public class SimpleKafkaPubSubUnitTest(
	ITestOutputHelper testOutputHelper,
	SimpleKafkaPubSubFixture testFixture) 
	: KafkaPubSubTestBase<SimpleKafkaPubSubFixture>(testOutputHelper, testFixture)
{
	[Fact]
	public async Task ProduceAndConsumeStringMessage_Success()
	{
		// Arrange
		var testTopic = "test-topic";
		var testKey = "test-key";
		var testValue = "test-value";

		var kafkaProducer = TestFixture.KafkaProducer;
		var kafkaConsumer = TestFixture.KafkaConsumer;
		kafkaConsumer.Subscribe(testTopic);

		var message = new Message<string, string>
		{
			Key = testKey,
			Value = testValue,
			Headers = new()
			{
				new Header("test-header-1", Encoding.UTF8.GetBytes("test-header-value-1"))
			},
			Timestamp = new Timestamp(DateTimeOffset.UtcNow),
		};

		// Act
		var deliveryResult = await kafkaProducer.ProduceAsync(testTopic, message);

		// Assert
		deliveryResult.Should().NotBeNull();
		deliveryResult.Status.Should().Be(PersistenceStatus.Persisted);
		var result = kafkaConsumer.Consume(TimeSpan.FromSeconds(10));
		result.Should().NotBeNull();
		result.Message.Should().BeEquivalentTo(message);
	}
}
