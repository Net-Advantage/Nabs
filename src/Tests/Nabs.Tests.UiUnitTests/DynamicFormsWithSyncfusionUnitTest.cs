using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Nabs.Ui.Abstractions;
using Nabs.Ui.Forms;
using Xunit.Abstractions;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Inputs;

namespace Nabs.Tests.UiUnitTests;

public sealed class DynamicFormsWithSyncfusionUnitTest : TestContext
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DynamicFormsWithSyncfusionUnitTest(ITestOutputHelper testOutputHelper)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddSyncfusionBlazor();
        Services.AddTransient<BlazorUiMappings>((sp) =>
        {
            var blazorUiMappings = new BlazorUiMappings();
            blazorUiMappings.ClearMappings();
            blazorUiMappings.AddFormGroupWrapper(typeof(FormGroupWrapper));
            blazorUiMappings.AddFormInputWrapper(typeof(FormInputWrapper));
            blazorUiMappings.AddFormInputMapping("string", typeof(SfTextBox));
            blazorUiMappings.AddFormInputMapping("int", typeof(SfNumericTextBox<int>));
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
        cut.Find("input#StringValue").Should().NotBeNull();

        cut.Find("label[for='IntValue']").Should().NotBeNull();
        cut.Find("input#IntValue").Should().NotBeNull();
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

        cut.Find("input#StringValue").Change(expectedStringValue);
        cut.Find("input#IntValue").Change(expectedIntValue);

        // Assert
        _testOutputHelper.WriteLine(cut.Markup);

        model.StringValue.Should().Be(expectedStringValue);
        model.IntValue.Should().Be(expectedIntValue);
    }
}
