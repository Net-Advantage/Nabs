namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.Full;

public sealed record FullActivityState(
    Guid Id,
    string FirstName,
    string LastName)
    : ActivityState
{
    public string FullName { get; set; } = default!;
};
