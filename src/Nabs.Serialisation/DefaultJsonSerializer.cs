namespace Nabs.Serialisation;

public static class DefaultJsonSerializer
{
    public static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, GlobalSettings.JsonSerializerOptions)!;
    }

    public static string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, GlobalSettings.JsonSerializerOptions);
    }
}
