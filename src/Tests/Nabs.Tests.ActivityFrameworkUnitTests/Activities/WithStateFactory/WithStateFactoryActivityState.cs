namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateFactory;

public sealed record WithStateFactoryActivityState(
    Guid Id,
    string Name,
    DateOnly? BirthDate) : ActivityState
{
    public int Age { get; }
}
