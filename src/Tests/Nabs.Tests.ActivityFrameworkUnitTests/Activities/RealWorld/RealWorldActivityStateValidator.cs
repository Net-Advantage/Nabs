namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.RealWorld;

public sealed class RealWorldActivityStateValidator
    : ActivityStateValidator<RealWorldActivityState>
{
    public RealWorldActivityStateValidator()
    {
        RuleFor(x => x.PersonEntity)
            .NotNull();
        RuleFor(x => x.PersonEntity.Username)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.PersonEntity.FirstName)
            .NotEmpty();
        RuleFor(x => x.PersonEntity.LastName)
            .NotEmpty();

        RuleFor(x => x.EmailMessage)
            .NotNull();
        RuleFor(x => x.EmailMessage!.From)
            .EmailAddress();
        RuleFor(x => x.EmailMessage!.To)
            .EmailAddress();
        RuleFor(x => x.EmailMessage!.Subject)
            .MinimumLength(3)
            .MaximumLength(50);
        RuleFor(x => x.EmailMessage!.Body)
            .MinimumLength(3)
            .MaximumLength(500);

        RuleFor(x => x.SessionId)
            .NotEmpty();

        RuleFor(x => x.ProcessedAt)
            .NotNull();

        RuleFor(x => x)
            .Must(x =>
            {
                var a = x.NewValueService.NewUtcNow();
                return x.ProcessedAt >= a.AddMinutes(-1)
                    && x.ProcessedAt <= a.AddMinutes(1);
            });
    }
}