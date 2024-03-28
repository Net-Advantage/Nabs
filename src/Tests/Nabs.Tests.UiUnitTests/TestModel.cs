using Nabs.Ui.Abstractions;

namespace Nabs.Tests.UiUnitTests;

public class TestModel
{
    [BlazorUIHint("string", "String value")]
    public string StringValue { get; set; } = "Initial Value";

    [BlazorUIHint("int", "Int value")]
    public int IntValue { get; set; } = 10;

    [BlazorUIGroup("Section", "Address")]
    public Address Address { get; set; } = new();
}

public class Address
{
    [BlazorUIHint("string", "Address Line 1")]
    public string? AddressLine1 { get; set; }

    [BlazorUIHint("string", "Address Line 2")]
    public string? AddressLine2 { get; set; }

    [BlazorUIHint("string", "City")]
    public string? City { get; set; }
}