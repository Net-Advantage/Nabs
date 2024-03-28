namespace Nabs.Ui.Abstractions;

public class BlazorUiMappings
{
    public Type? FormGroupWrapper { get; private set; }
    public Type? FormInputWrapper { get; private set; }
    public Dictionary<string, Type> InputMappings { get; } = [];

    public void ClearMappings()
    {
        FormGroupWrapper = null;
        FormInputWrapper = null;
        InputMappings.Clear();
    }

    public void AddFormGroupWrapper(Type componentType)
    {
        FormGroupWrapper = componentType;
    }

    public void AddFormInputWrapper(Type componentType)
    {
        FormInputWrapper = componentType;
    }

    public void AddFormInputMapping<TValue>(Type componentType)
    {
        var typeName = typeof(TValue).Name;
        InputMappings.TryAdd(typeName, componentType);
    }

    public void AddFormInputMapping(string typeName, Type componentType)
    {
        InputMappings.TryAdd(typeName, componentType);
    }
}
