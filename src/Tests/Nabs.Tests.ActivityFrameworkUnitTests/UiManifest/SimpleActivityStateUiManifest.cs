namespace Nabs.Tests.ActivityFrameworkUnitTests.UiManifest;

public sealed class SimpleActivityStateUiManifest : ActivityStateUiManifest<SimpleActivityState>
{
    public SimpleActivityStateUiManifest()
    {
        TitleFor("Simple Activity State Form")
            .WithDescription("The simple activity state form's description.");

        PropertyFor(x => x.Username)
            .WithSequence(1)
            .WithLabel("Username")
            .WithPlaceholder("Enter username")
            .WithHelpText("Enter your username here.")
            .WithValidationRule(UiValidationRule.Required, "Username is required.");

        PropertyFor(x => x.FirstName)
            .WithSequence(2)
            .WithLabel("First name")
            .WithPlaceholder("Enter first name")
            .WithHelpText("Enter your first name here.")
            .WithValidationRule(UiValidationRule.Required, "First name is required.");
    }
}
