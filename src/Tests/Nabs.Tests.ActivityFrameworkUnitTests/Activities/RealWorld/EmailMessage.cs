namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.RealWorld;

public sealed class EmailMessage
{
    public required string From { get; set; }
    public required string To { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}