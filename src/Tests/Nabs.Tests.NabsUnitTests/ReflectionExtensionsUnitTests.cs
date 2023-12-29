namespace Nabs.Tests.NabsUnitTests;

public class ReflectionExtensionsUnitTests
{
	[Fact]
	public void CreateInstance_Success()
	{
		//Arrange
		var type = typeof(TestClasses.TestClass);

		//Act
		var instance = type.CreateInstance<TestClasses.TestClass>();

		//Assert
		instance.Should().NotBeNull();
		instance.Should().BeOfType<TestClasses.TestClass>();
	}

	[Fact]
	public void CreateInstanceWithString_Success()
	{
		//Arrange
		var type = typeof(TestClasses.TestClass);

		//Act
		var instance = type.CreateInstance<TestClasses.TestClass>("With string!");

		//Assert
		instance.Should().NotBeNull();
		instance.Should().BeOfType<TestClasses.TestClass>();
	}
}