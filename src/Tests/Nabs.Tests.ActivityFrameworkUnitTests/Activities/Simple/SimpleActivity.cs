namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.Simple;

public sealed class SimpleActivity(
    SimpleActivityState activityState)
        : Activity<SimpleActivityState>(activityState)
{
}