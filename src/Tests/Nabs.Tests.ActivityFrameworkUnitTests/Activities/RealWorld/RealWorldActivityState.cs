namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.RealWorld;

public sealed record RealWorldActivityState(
    PersonEntity PersonEntity,
    NewValueService NewValueService) 
    : ActivityState
{
    public Guid? SessionId { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public EmailMessage? EmailMessage { get; set; }

}
