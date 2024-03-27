namespace Nabs.Ui.Abstractions;

public static class BlazorUiHintMappings
{
    public static Type? FormInputWrapper { get; private set; }
    public static Dictionary<string, Type> InputMappings { get; } = [];

    public static void ClearMappings()
    {
        FormInputWrapper = null;
        InputMappings.Clear();
    }

    public static void AddFormInputWrapper(Type componentType)
    {
        FormInputWrapper = componentType;
    }

    public static void AddFormInputMapping<TValue>(Type componentType)
    {
        var typeName = typeof(TValue).Name;
        InputMappings.TryAdd(typeName, componentType);
    }

    public static void AddFormInputMapping(string typeName, Type componentType)
    {
        InputMappings.TryAdd(typeName, componentType);
    }
}
