using Nabs.Serialisation;

[assembly: Xunit.TestFramework("Nabs.Tests.TestsUnitTests.RunOnce", "Nabs.Tests.TestsUnitTests")]

namespace Nabs.Tests.TestsUnitTests;

public sealed class RunOnce : XunitTestFramework, IDisposable
{
    public RunOnce(IMessageSink messageSink) : base(messageSink)
    {
        GlobalSettings.RegisterCsvConfiguration(new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
            MissingFieldFound = null,
            IgnoreBlankLines = true,
            Delimiter = "|",
            TrimOptions = TrimOptions.Trim
        });
    }

    public new void Dispose()
    {
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
