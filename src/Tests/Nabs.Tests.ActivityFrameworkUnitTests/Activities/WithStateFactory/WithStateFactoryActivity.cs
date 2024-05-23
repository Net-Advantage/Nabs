namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateFactory;

public sealed class WithStateFactoryActivity
    : Activity<WithStateFactoryActivityState>
{
    public WithStateFactoryActivity(
        WithStateFactoryActivityState activityState)
        : base(activityState)
    {
        AddFactory(new WithStateFactoryActivityStateFactory());
    }
}
