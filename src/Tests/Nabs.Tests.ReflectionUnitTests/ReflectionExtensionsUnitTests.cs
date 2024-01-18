using FluentAssertions;
using Nabs.Reflection;
using System.Reflection;

namespace Nabs.Tests.ReflectionUnitTests;

public sealed class ReflectionExtensionsUnitTests
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
	public async Task InvokeMethodAndGetName_Success()
	{
		//Arrange
		var name = "The name";
		var type = typeof(TestClasses.TestClass);

		//Act
		var instance = type.CreateInstance<TestClasses.TestClass>(name);
		var value = await type.InvokeMethodAsync<string>("GetNameAsync", instance);

		//Assert
		value.Should().NotBeNull();
		value.Should().Be(name);
	}

	[Fact]
	public async Task InvokeMethodSetAgeAndGetName_Success()
	{
		//Arrange
		var age = 10;
		var name = "The name";
		var type = typeof(TestClasses.TestClass);

		//Act
		var instance = type.CreateInstance<TestClasses.TestClass>(name);
		var value = await type.InvokeMethodAsync<string>("SetAgeAndGetNameAsync", instance, BindingFlags.Instance | BindingFlags.Public, age);

		//Assert
		value.Should().NotBeNull();
		value.Should().Be(name);
	}

	[Fact]
	public async Task InvokeMethodAndSetAge_Success()
	{
		//Arrange
		var age = 10;
		var type = typeof(TestClasses.TestClass);

		//Act
		var instance = type.CreateInstance<TestClasses.TestClass>();
		await type.InvokeMethodAsync("SetAgeAsync", instance, BindingFlags.Instance | BindingFlags.Public, age);

		//Assert
		instance.Age.Should().Be(age);
	}

	[Fact]
	public async Task InvokeMethodAndNotExistsAsync_Failed()
	{
		//Arrange
		var name = "The name";
		var type = typeof(TestClasses.TestClass);

		//Act
		var instance = type.CreateInstance<TestClasses.TestClass>(name);
		var action = async () => await type.InvokeMethodAsync<string>("NotExistsAsync", instance);

		//Assert
		await action.Should().ThrowAsync<ArgumentException>();
	}

	[Fact]
	public async Task InvokeMethodAndGetNull_Success()
	{
		//Arrange
		var type = typeof(TestClasses.TestClass);

		//Act
		var instance = type.CreateInstance<TestClasses.TestClass>();
		var value = await type.InvokeMethodAsync<string>("GetNullAsync", instance);

		//Assert
		value.Should().BeNull();
	}

	[Fact]
	public async Task InvokeMethodAndDoNothing_Success()
	{
		//Arrange
		var type = typeof(TestClasses.TestClass);

		//Act
		var instance = type.CreateInstance<TestClasses.TestClass>();
		await type.InvokeMethodAsync("DoNothingAsync", instance);

		//Assert
		instance.Should().NotBeNull();
	}

	[Fact]
	public async Task InvokeMethodAndNotExistsAsync_Task_Failed()
	{
		//Arrange
		var type = typeof(TestClasses.TestClass);

		//Act
		var instance = type.CreateInstance<TestClasses.TestClass>();
		var action = async () => await type.InvokeMethodAsync("NotExistsAsync", instance);

		//Assert
		await action.Should().ThrowAsync<ArgumentException>();
	}
}
