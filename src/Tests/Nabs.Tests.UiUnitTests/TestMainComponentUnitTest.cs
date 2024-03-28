using Bunit;
using Xunit.Abstractions;
using Nabs.Tests.UiUnitTestsComponents;

namespace Nabs.Tests.UiUnitTests;

public class TestMainComponentUnitTest : TestContext
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TestMainComponentUnitTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void RenderComponent1()
    {
        // Arrange

        // Act
        var cut = RenderComponent<TestMainComponent>();

        // Assert
        //cut.MarkupMatches("<div><div>Hello World</div></div>");
        _testOutputHelper.WriteLine(cut.Markup);
    }
}
