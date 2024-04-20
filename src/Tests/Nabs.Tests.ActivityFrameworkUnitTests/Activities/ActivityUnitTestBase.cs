namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities;

public abstract class ActivityUnitTestBase
{
    protected ActivityUnitTestBase(
        ITestOutputHelper outputHelper)
    {
        OutputHelper = outputHelper;
        ActivityTestValidation = new(OutputHelper);
    }

    public ITestOutputHelper OutputHelper { get; }
    public ActivityTestValidation ActivityTestValidation { get; }
}
