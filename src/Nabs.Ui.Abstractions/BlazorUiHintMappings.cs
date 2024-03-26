namespace Nabs.Ui.Abstractions;

public static class BlazorUiHintMappings
{
    public static Dictionary<string, Type> Mappings { get; } = [];

    public static void ClearMappings()
    {
        Mappings.Clear();
    }

    public static void AddMapping<TValue>(Type componentType)
    {
        var typeName = typeof(TValue).Name;
        Mappings.TryAdd(typeName, componentType);
    }

    public static void AddMapping(string typeName, Type componentType)
    {
        Mappings.TryAdd(typeName, componentType);
    }
}
