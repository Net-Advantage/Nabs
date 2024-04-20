namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateValidator;

public sealed record WithStateValidatorActivityState(
    string ValueToValidate) 
    : ActivityState;
