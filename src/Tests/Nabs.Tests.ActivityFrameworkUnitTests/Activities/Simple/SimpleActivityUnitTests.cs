namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.Simple;

public sealed class SimpleActivityUnitTests(
    ITestOutputHelper outputHelper)
    : ActivityUnitTestBase(outputHelper)
{
    [Fact]
    public void CreateActivityTest()
    {
        // Arrange
        var state = new SimpleActivityState(Guid.NewGuid(), "JoeS", "Joe");
        var expectedState = state;
        var activity = new SimpleActivity(state);

        // Act
        activity.Run();

        // Assert
        ActivityTestValidation
            .ValidateActivity(activity, expectedState, expectedState);
    }
}
