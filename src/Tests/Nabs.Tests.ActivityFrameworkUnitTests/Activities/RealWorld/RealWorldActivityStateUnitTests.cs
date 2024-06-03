namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.RealWorld;

public sealed class RealWorldActivityStateUnitTests(
    ITestOutputHelper testOutputHelper)
        : ActivityUnitTestBase(testOutputHelper)
{
    [Fact]
    public async Task CreateActivityTest()
    {
        // Arrange
        var newValueService = new NewValueService(true);
        newValueService.SetTestGuid(Guid.NewGuid());
        newValueService.SetTestDateTime(DateTime.Now);

        var personEntity = new PersonEntity()
        {
            Username = "joe.soap@m.com",
            FirstName = "Joe",
            LastName = "Soap",
            DateOfBirth = new DateOnly(1980, 1, 1)
        };
        var expectedInitialState = new RealWorldActivityState(
            personEntity, newValueService);
        var expectedState = expectedInitialState with
        {
            SessionId = newValueService.NewGuid(),
            ProcessedAt = newValueService.NewUtcNow(),
            EmailMessage = new EmailMessage
            {
                From = "from@m.com",
                To = personEntity.Username,
                Subject = "The Subject",
                Body = $"""
                Dear {personEntity.FirstName},

                The body is here!
                
                Cheers
                """
            }
        };
        var state = expectedInitialState;
        var activity = new RealWorldActivity(state);

        // Act
        await activity.RunAsync();

        // Assert
        ActivityTestValidation
            .ValidateActivity(activity, expectedInitialState, expectedState);

        var activityState = activity.ActivityState!;
        activityState.EmailMessage.Should().NotBeNull();
        activityState.SessionId.Should().NotBeNull();
        activityState.SessionId.Should().NotBeEmpty();
    }
}