using Bunit;
using FluentAssertions;
using Nabs.Ui;

namespace Nabs.Tests.UiUnitTests;

public class Component1UnitTest : TestContext
{
    [Fact]
    public void RenderComponent1()
    {
        // Arrange

        // Act
        var cut = RenderComponent<Component1>();

        // Assert
        cut.Find("div").GetAttribute("class").Should().Be("my-component");
    }
}
