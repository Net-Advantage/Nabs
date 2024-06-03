namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateFactory;

public sealed class WithStateFactoryActivityUnitTests(
    ITestOutputHelper outputHelper)
    : ActivityUnitTestBase(outputHelper)
{
    [Fact]
    public async Task CreateActivityTest()
    {
        // Arrange
        var expectedInitialState = new WithStateFactoryActivityState(
            Guid.Empty,
            "",
            null);
        var state = expectedInitialState;
        var activity = new WithStateFactoryActivity(state);

        // Act
        await activity.RunAsync();

        // Assert
        ActivityTestValidation
            .ValidateActivity(activity, expectedInitialState, state);
    }
}
