namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.FullActivity;

public sealed record FullActivityState(
    Guid Id,
    string FirstName,
    string LastName)
    : ActivityState
{
    public string FullName { get; set; } = default!;
};
