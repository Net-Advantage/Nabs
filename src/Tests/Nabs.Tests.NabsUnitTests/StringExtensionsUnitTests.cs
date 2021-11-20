using System;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Nabs.Tests.NabsUnitTests
{
    public class StringExtensionsUnitTests : TestBase
    {
        public StringExtensionsUnitTests(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData("a")]
        [InlineData("-")]
        public void OrDefault_ValueSuccess(string value)
        {
            //Arrange
            var expectedValue = value;

            //Act
            var actualValue = value.OrDefault("xyz");

            //Assert
            actualValue.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("", "a")]
        [InlineData(null, "a")]
        [InlineData(" ", "a")]
        public void OrDefault_DefaultSuccess(string value, string defaultValue)
        {
            //Arrange
            var expectedValue = defaultValue;

            //Act
            var actualValue = value.OrDefault(defaultValue);

            //Assert
            actualValue.Should().Be(expectedValue);
            actualValue.Should().NotBe(value);
        }
    }
}