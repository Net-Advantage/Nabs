namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.Full;

public sealed class FullActivity 
    : Activity<FullActivityState>
{
    public FullActivity(FullActivityState activityState)
        : base(activityState)
    {
        AddValidator(new FullActivityStateValidator());

        AddBehaviour(new FullActivityStateTransformer());
    }
}
