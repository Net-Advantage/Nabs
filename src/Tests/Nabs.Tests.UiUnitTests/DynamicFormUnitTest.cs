using Bunit;
using FluentAssertions;
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

        BlazorUiHintMappings.AddMapping("string", typeof(FluentTextField));
        BlazorUiHintMappings.AddMapping("int", typeof(FluentNumberField<int>));
    }

    [Fact]
    public void RenderDynamicForm()
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
}