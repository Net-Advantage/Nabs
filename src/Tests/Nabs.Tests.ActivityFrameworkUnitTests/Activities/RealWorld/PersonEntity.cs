namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.RealWorld;

public sealed class PersonEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
}
