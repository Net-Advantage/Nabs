using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Nabs.Ui.Abstractions;
using Nabs.Ui.Forms;
using Xunit.Abstractions;

namespace Nabs.Tests.UiUnitTests;

public class DynamicFormWithDefaultUiUnitTest : TestContext
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DynamicFormWithDefaultUiUnitTest(ITestOutputHelper testOutputHelper)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        _testOutputHelper = testOutputHelper;
        Services.AddTransient<BlazorUiMappings>((sp) =>
        {
            var blazorUiMappings = new BlazorUiMappings();
            blazorUiMappings.ClearMappings();
            blazorUiMappings.AddFormGroupWrapper(typeof(FormGroupWrapper));
            blazorUiMappings.AddFormInputWrapper(typeof(FormInputWrapper));
            blazorUiMappings.AddFormInputMapping("string", typeof(InputText));
            blazorUiMappings.AddFormInputMapping("int", typeof(InputNumber<int>));
            return blazorUiMappings;
        });
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
