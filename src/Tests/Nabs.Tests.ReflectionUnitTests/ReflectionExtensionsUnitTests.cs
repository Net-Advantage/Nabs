using FluentAssertions;
using Nabs.Reflection;

namespace Nabs.Tests.ReflectionUnitTests;

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
}