namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.RealWorld;

public sealed class RealWorldActivityStateEmailTransformer
    : ActivityStateTransformer<RealWorldActivityState>
{
    public override RealWorldActivityState Run(RealWorldActivityState activityState)
    {
        var result = activityState with
        {
            EmailMessage = new EmailMessage
            {
                From = "from@m.com",
                To = activityState.PersonEntity.Username,
                Subject = "The Subject",
                Body = $"""
                Dear {activityState.PersonEntity.FirstName},

                The body is here!

                Cheers
                """
            }
        };

        return result;
    }
}