namespace Nabs.Tests.SerialisationUnitTests;

public abstract class BaseSerialisationUnitTest : IAsyncLifetime
{
    public Task InitializeAsync()
    {
        ResetStaticMember(typeof(GlobalSettings), "_csvConfiguration");
        ResetStaticMember(typeof(GlobalSettings), "_jsonSerializerOptions");

        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    private static void ResetStaticMember(Type type, string fieldName)
    {
        var field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic);
        field?.SetValue(null, null);
    }
}