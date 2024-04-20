namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities;

public class ActivityTestValidation
{
    private readonly ITestOutputHelper _outputHelper;

    public ActivityTestValidation(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    public void ValidateActivity<TActivity, TActivityState>(
        TActivity activity,
        TActivityState expectedInitialState,
        TActivityState expectedState)
        where TActivity : Activity<TActivityState>
        where TActivityState : ActivityState
    {
        // Assert
        var validationResult = activity.ValidationResult;
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();

        var initialActivityState = activity.InitialActivityState;
        initialActivityState.Should().NotBeNull();
        initialActivityState.Should().BeOfType<TActivityState>();
        initialActivityState.Should().BeEquivalentTo(expectedInitialState);

        var activityState = activity.ActivityState;
        activityState.Should().NotBeNull();
        activityState.Should().BeOfType<TActivityState>();
        activityState.Should().BeEquivalentTo(expectedState);

        var json = DefaultJsonSerializer.Serialize(activityState);
        _outputHelper.WriteLine(json);
    }
}
