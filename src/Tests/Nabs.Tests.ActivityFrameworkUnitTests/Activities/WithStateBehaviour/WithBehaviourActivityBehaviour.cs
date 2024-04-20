﻿namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateBehaviour;

public sealed class WithBehaviourActivityBehaviour
    : IActivityStateBehaviour<WithBehaviourActivityState>
{
    public WithBehaviourActivityState Run(WithBehaviourActivityState activityState)
    {
        activityState = activityState with
        {
            ValueToUpdate = "Updated Value"
        };
        return activityState;
    }
}
