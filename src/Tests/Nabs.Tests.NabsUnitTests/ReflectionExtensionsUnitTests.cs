using FluentAssertions;
using Nabs.Tests.Fixtures;
using Xunit.Abstractions;

namespace Nabs.Tests.NabsUnitTests
{
	public class ReflectionExtensionsUnitTests : TestBase
	{
		public ReflectionExtensionsUnitTests(
			ITestOutputHelper output)
			: base(output)
		{
		}

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
}