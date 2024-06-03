namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.Simple;

public sealed class SimpleActivityUnitTests(
    ITestOutputHelper outputHelper)
    : ActivityUnitTestBase(outputHelper)
{
    [Fact]
    public async Task CreateActivityTest()
    {
        // Arrange
        var state = new SimpleActivityState(Guid.NewGuid(), "JoeS", "Joe");
        var expectedState = state;
        var activity = new SimpleActivity(state);

        // Act
        await activity.RunAsync();

        // Assert
        ActivityTestValidation
            .ValidateActivity(activity, expectedState, expectedState);
    }
}
