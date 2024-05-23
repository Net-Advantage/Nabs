using LanguageExt;

namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateBehaviour;

public sealed class WithBehaviourActivityUnitTests(
    ITestOutputHelper outputHelper)
    : ActivityUnitTestBase(outputHelper)
{
    [Fact]
    public void CreateActivityTest()
    {
        // Arrange
        var expectedInitialState = new WithBehaviourActivityState(
            "Initial Value");
        var expectedFinalState = new WithBehaviourActivityState(
            "Updated Value");
        var state = expectedInitialState;
        var activity = new WithBehaviourActivity(state);

        // Act
        activity.Run();

        // Assert
        ActivityTestValidation
            .ValidateActivity(
                activity,
                expectedInitialState,
                expectedFinalState);

        activity.ActivityState!.ValueToUpdate.Should().Be("Updated Value");
    }
}
