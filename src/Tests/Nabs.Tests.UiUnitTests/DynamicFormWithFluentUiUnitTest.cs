using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using Nabs.Ui.Abstractions;
using Nabs.Ui.Forms;
using Xunit.Abstractions;

namespace Nabs.Tests.UiUnitTests;

public class DynamicFormWithFluentUiUnitTest : TestContext
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DynamicFormWithFluentUiUnitTest(ITestOutputHelper testOutputHelper)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddFluentUIComponents();
        Services.AddTransient<BlazorUiMappings>((sp) =>
        {
            var blazorUiMappings = new BlazorUiMappings();
            blazorUiMappings.ClearMappings();
            blazorUiMappings.AddFormGroupWrapper(typeof(FormGroupWrapper));
            blazorUiMappings.AddFormInputMapping("string", typeof(FluentTextField));
            blazorUiMappings.AddFormInputMapping("int", typeof(FluentNumberField<int>));
            return blazorUiMappings;
        });
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void RenderDynamicFormUi()
    {
        // Arrange
        var model = new TestModel();

        // Act
        var cut = RenderComponent<DynamicForm<TestModel>>(parameters =>
        {
            parameters.Add(p => p.Model, model);
        });

        // Assert
        cut.Find("label[for='StringValue']").Should().NotBeNull();
        cut.Find("fluent-text-field#StringValue").Should().NotBeNull();

        cut.Find("label[for='IntValue']").Should().NotBeNull();
        cut.Find("fluent-number-field#IntValue").Should().NotBeNull();
    }

    [Fact]
    public void ChangeFormValues()
    {
        // Arrange
        var model = new TestModel();
        var expectedStringValue = $"Updated on: {DateTime.Now}";
        var expectedIntValue = 200;

        // Act
        var cut = RenderComponent<DynamicForm<TestModel>>(parameters =>
        {
            parameters.Add(p => p.Model, model);
        });

        cut.Find("fluent-text-field#StringValue").Change(expectedStringValue);
        cut.Find("fluent-number-field#IntValue").Change(expectedIntValue);

        // Assert
        _testOutputHelper.WriteLine(cut.Markup);

        model.StringValue.Should().Be(expectedStringValue);
        model.IntValue.Should().Be(expectedIntValue);
    }
}