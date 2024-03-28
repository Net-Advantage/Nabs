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

public class BlazorUIGroupAttribute : Attribute
{
    public BlazorUIGroupAttribute(string groupComponent, string uiTitle)
    {
        GroupComponent = groupComponent;
        UiTitle = uiTitle;
    }

    public string GroupComponent { get; }
    public string UiTitle { get; }
}