using Nabs.Ui.Abstractions;

namespace Nabs.Tests.UiUnitTests;

public class TestModel
{
    [BlazorUIHint("string", "String value")]
    public string StringValue { get; set; } = "Initial Value";

    [BlazorUIHint("int", "Int value")]
    public int IntValue { get; set; } = 10;
}
