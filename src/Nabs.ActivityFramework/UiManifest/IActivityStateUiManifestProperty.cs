namespace Nabs.ActivityFramework.UiManifest;

public interface IActivityStateUiManifestProperty<T>
{
    IActivityStateUiManifestProperty<T> WithSequence(int sequence);
    IActivityStateUiManifestProperty<T> WithLabel(string label);
    IActivityStateUiManifestProperty<T> WithPlaceholder(string placeholder);
    IActivityStateUiManifestProperty<T> WithHelpText(string helpText);
    IActivityStateUiManifestProperty<T> WithValidationRule(UiValidationRule rule, string message);
}
