﻿namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.RealWorld;

public sealed class RealWorldActivity
    : Activity<RealWorldActivityState>
{
    public RealWorldActivity(RealWorldActivityState activityState)
        : base(activityState)
    {
        AddValidator<RealWorldActivityStateValidator>();

        AddBehaviour(new RealWorldActivityStateTransformer());
        AddBehaviour(new RealWorldActivityStateEmailTransformer());
    }
}
