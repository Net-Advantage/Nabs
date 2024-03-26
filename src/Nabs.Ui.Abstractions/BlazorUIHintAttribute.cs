namespace Nabs.Ui.Abstractions;

public class BlazorUIHintAttribute : Attribute
{
    public BlazorUIHintAttribute(string uiComponent, string uiLabel)
    {
        UIComponent = uiComponent;
        UiLabel = uiLabel;
    }

    public string UIComponent { get; }
    public string UiLabel { get; }

}
