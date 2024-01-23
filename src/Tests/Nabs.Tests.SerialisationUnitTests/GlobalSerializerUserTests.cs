namespace Nabs.Tests.SerialisationUnitTests;

public sealed class GlobalSerializerUserTests : BaseSerialisationUnitTest
{
	[Fact]
	public void SerializeTest()
	{
		// Arrange
		var instance = new JsonTest()
		{
			Name = "Test"
		};

		// Act
		var result = DefaultJsonSerializer.Serialize(instance);

		// Assert
		result.Should().NotBeNullOrWhiteSpace();
		result.Should().NotContain("Name");
		result.Should().Contain("name");
		result.Should().Contain("Test");
	}

	[Fact]
	public void DeserializeTest()
	{
		// Arrange
		var json =
			"""
			{
				"name": "Test"
			}
			""";

		// Act
		var instance = DefaultJsonSerializer.Deserialize<JsonTest>(json);

		// Assert
		instance.Should().NotBeNull();
		instance.Should().BeOfType<JsonTest>();
		instance.Name.Should().Be("Test");
	}
}
