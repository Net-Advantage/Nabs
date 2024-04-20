namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateValidator;

public sealed class WithStateValidatorActivityStateValidator
    : ActivityStateValidator<WithStateValidatorActivityState>
{
    public WithStateValidatorActivityStateValidator()
    {
        RuleFor(x => x.ValueToValidate)
            .NotEmpty()
            .WithMessage(a => $"{nameof(a.ValueToValidate)} requires a value.");
    }
}
