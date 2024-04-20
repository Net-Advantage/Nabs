namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.FullActivity;

public sealed class FullActivityStateActivityUnitTests(
    ITestOutputHelper outputHelper)
    : ActivityUnitTestBase(outputHelper)
{
    [Fact]
    public void CreateActivityTest()
    {
        // Arrange
        var expectedInitialState = new FullActivityState(Guid.NewGuid(), "Joe", "Soap");
        var expectedState = expectedInitialState with
        {
            FullName = "Joe Soap"
        };
        var state = expectedInitialState;
        var activity = new FullActivity(state);

        // Act
        activity.Run();

        // Assert
        ActivityTestValidation
            .ValidateActivity(activity, expectedInitialState, expectedState);

        activity.ActivityState!.FullName.Should().Be($"{state.FirstName} {state.LastName}");
    }
}
