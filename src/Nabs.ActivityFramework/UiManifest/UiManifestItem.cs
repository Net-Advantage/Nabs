namespace Nabs.ActivityFramework.UiManifest;

public class UiManifestItem
{
    public int Sequence { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Placeholder { get; set; } = string.Empty;
    public string HelpText { get; set; } = string.Empty;
    public UiValidationRule ValidationRule { get; set; }
    public string ValidationRuleMessage { get; set; } = string.Empty;
}
