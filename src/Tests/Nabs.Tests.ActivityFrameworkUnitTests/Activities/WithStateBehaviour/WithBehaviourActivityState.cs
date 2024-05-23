namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateBehaviour;

public sealed record WithBehaviourActivityState(
    string ValueToUpdate)
    : ActivityState;