namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateValidator;

public sealed class WithStateValidatorActivity
    : Activity<WithStateValidatorActivityState>
{
    public WithStateValidatorActivity(
        WithStateValidatorActivityState activityState)
        : base(activityState)
    {
        AddValidator(new WithStateValidatorActivityStateValidator());
    }
}
