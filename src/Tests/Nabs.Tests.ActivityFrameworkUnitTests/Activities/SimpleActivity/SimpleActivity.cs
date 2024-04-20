namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.SimpleActivity;

public sealed class SimpleActivity(
    SimpleActivityState activityState)
        : Activity<SimpleActivityState>(activityState)
{
}