namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities.WithStateValidator;

public sealed class WithStateValidatorActivityUnitTests(
    ITestOutputHelper outputHelper)
{
    private readonly ITestOutputHelper _outputHelper = outputHelper;

    [Fact]
    public void CreateActivityTest()
    {
        // Arrange
        var initialActivityState = new WithStateValidatorActivityState(
            string.Empty);
        var state = initialActivityState;
        var activity = new WithStateValidatorActivity(state);

        // Act
        activity.Run();

        // Assert
        var result = activity.ActivityState;
        result.Should().NotBeNull();
        result.Should().BeOfType<WithStateValidatorActivityState>();
        result.Should().BeEquivalentTo(activity.InitialActivityState);

        activity.ValidationResult.Should().NotBeNull();
        activity.ValidationResult.IsValid.Should().BeFalse();
        activity.ValidationResult.Errors.Should().NotBeEmpty();
        activity.ValidationResult.Errors.Should().HaveCount(1);
        activity.ValidationResult.Errors.First().ErrorMessage.Should().Be("ValueToValidate requires a value.");

        var json = DefaultJsonSerializer.Serialize(result);
        _outputHelper.WriteLine(json);
    }
}