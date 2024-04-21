namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.Full;

public sealed class FullActivityStateActivityUnitTests : ActivityUnitTestBase
{
    private FullActivityState _expectedInitialState = default!;
    private FullActivityState _expectedState = default!;

    public FullActivityStateActivityUnitTests(
        ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _expectedInitialState = new FullActivityState(Guid.NewGuid(), "Joe", "Soap");
        _expectedState = _expectedInitialState with
        {
            FullName = "Joe Soap"
        };
    }

    [Fact]
    public void CreateActivityTest()
    {
        // Arrange
        var state = _expectedInitialState;
        var activity = new FullActivity(state);

        // Act
        activity.Run();

        // Assert
        ActivityTestValidation
            .ValidateActivity(activity, _expectedInitialState, _expectedState);

        activity.ActivityState!.FullName.Should().Be($"{state.FirstName} {state.LastName}");
    }

    [Fact]
    public void FullActivityStateTransformerTest()
    {
        // Arrange
        var state = _expectedInitialState;
        var transformer = new FullActivityStateTransformer();

        // Act
        var actualState = transformer.Run(state);

        // Assert
        actualState.Should().BeEquivalentTo(_expectedState);
    }
}
