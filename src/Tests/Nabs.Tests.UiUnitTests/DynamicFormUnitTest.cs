using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using Nabs.Ui;
using Nabs.Ui.Abstractions;
using Nabs.Ui.Forms;

namespace Nabs.Tests.UiUnitTests;

public class DynamicFormUnitTest : TestContext
{

    public DynamicFormUnitTest()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;

        Services.AddFluentUIComponents();
    }

    [Fact]
    public void RenderDynamicFormWithFluentUi()
    {
        // Arrange
        BlazorUiHintMappings.ClearMappings();
        BlazorUiHintMappings.AddFormInputMapping("string", typeof(FluentTextField));
        BlazorUiHintMappings.AddFormInputMapping("int", typeof(FluentNumberField<int>));
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
    public void RenderDynamicFormWithDefaultUi()
    {
        // Arrange
        BlazorUiHintMappings.ClearMappings();
        BlazorUiHintMappings.AddFormInputWrapper(typeof(FormInputWrapper));
        BlazorUiHintMappings.AddFormInputMapping("string", typeof(InputText));
        BlazorUiHintMappings.AddFormInputMapping("int", typeof(InputNumber<int>));
        var model = new TestModel();

        // Act
        var cut = RenderComponent<DynamicForm<TestModel>>(parameters =>
        {
            parameters.Add(p => p.Model, model);
        });

        // Assert
        cut.Find("label[for='StringValue']").Should().NotBeNull();
        cut.Find("input#StringValue").Should().NotBeNull();

        cut.Find("label[for='IntValue']").Should().NotBeNull();
        cut.Find("input#IntValue").Should().NotBeNull();
    }
}