namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateBehaviour;

public sealed class WithBehaviourActivity
    : Activity<WithBehaviourActivityState>
{
    public WithBehaviourActivity(
        WithBehaviourActivityState activityState) 
        : base(activityState)
    {
        AddBehaviour(new WithBehaviourActivityBehaviour());
    }
}
