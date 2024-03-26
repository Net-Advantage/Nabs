using Nabs.Ui.Abstractions;

namespace Nabs.Ui;

public class TestModel
{
    [BlazorUIHint("string", "String value")]
    public string StringValue { get; set; } = "Initial Value";
}
