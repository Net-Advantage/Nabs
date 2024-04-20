namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateFactory;

public sealed class WithStateFactoryActivityUnitTests(
    ITestOutputHelper outputHelper)
    : ActivityUnitTestBase(outputHelper)
{
    [Fact]
    public void CreateActivityTest()
    {
        // Arrange
        var expectedInitialState = new WithStateFactoryActivityState(
            Guid.Empty,
            "",
            null);
        var state = expectedInitialState;
        var activity = new WithStateFactoryActivity(state);

        // Act
        activity.Run();

        // Assert
        ActivityTestValidation
            .ValidateActivity(activity, expectedInitialState, state);
    }
}
