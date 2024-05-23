namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateFactory;

public sealed class WithStateFactoryActivityStateFactory : IActivityStateFactory<WithStateFactoryActivityState>
{
    public WithStateFactoryActivityState Run()
    {
        return new WithStateFactoryActivityState(
            Guid.Empty,
            "",
            null);
    }
}