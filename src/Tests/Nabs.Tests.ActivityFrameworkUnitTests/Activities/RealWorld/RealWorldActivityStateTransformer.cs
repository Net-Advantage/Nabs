namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.RealWorld;

public sealed class RealWorldActivityStateTransformer
    : ActivityStateTransformer<RealWorldActivityState>
{
    public override RealWorldActivityState Run(RealWorldActivityState activityState)
    {
        var result = activityState with
        {
            SessionId = activityState.NewValueService.NewGuid(),
            ProcessedAt = activityState.NewValueService.NewUtcNow()
        };

        return result;
    }
}
