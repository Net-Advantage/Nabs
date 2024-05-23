namespace Nabs.Tests;

public abstract class FixtureTestBase<TTestFixture>
    : IClassFixture<TTestFixture>, IAsyncLifetime
    where TTestFixture : TestFixtureBase
{

    protected FixtureTestBase(ITestOutputHelper testOutputHelper, TTestFixture testFixture)
    {
        var testClassName = GetType().Name;
        var message = $"[{testClassName}] is constructing...";

        TestFixture = testFixture;
        TestFixture.TestOutputHelper = testOutputHelper;

        TestFixture.OutputLine(message);
        TestFixture.OutputLine(new string('=', message.Length));

    }

    protected TTestFixture TestFixture { get; } = default!;

    public void OutputScenario(string scenario = "default", [CallerMemberName] string? caller = null)
    {
        TestFixture.OutputLine($"[{caller}] - Scenario: {scenario}");
    }

    public void OutputStep(string stepName)
    {
        TestFixture.OutputLine($"Step: {stepName}");
    }

    protected virtual async Task StartTest()
    {
        await Task.CompletedTask;
    }

    protected virtual async Task TeardownTest()
    {
        await Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        TestFixture.OutputLine("Test is initialising");
        TestFixture.Initialise();
        await StartTest();
    }

    public async Task DisposeAsync()
    {
        await TeardownTest();
        TestFixture.OutputLine("Test has ended");
    }
}