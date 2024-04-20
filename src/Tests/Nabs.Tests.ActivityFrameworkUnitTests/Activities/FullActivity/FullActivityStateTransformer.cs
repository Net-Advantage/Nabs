namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.FullActivity;

public sealed class FullActivityStateTransformer : ActivityStateTransformer<FullActivityState>
{
    public override FullActivityState Run(FullActivityState activityState)
    {
        var result = activityState with
        {
            FullName = $"{activityState.FirstName} {activityState.LastName}"
        };

        return result;
    }
}