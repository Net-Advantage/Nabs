using CsvHelper.Configuration;
using System.Globalization;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: Xunit.TestFramework("Nabs.Tests.TestsTests.RunOnce", "Nabs.Tests.TestsTests")]

namespace Nabs.Tests.TestsTests;

public sealed class RunOnce : XunitTestFramework, IDisposable
{
	public RunOnce(IMessageSink messageSink) : base(messageSink)
	{
		CommonTestDependencies.RegisterCsvConfiguration(new CsvConfiguration(CultureInfo.InvariantCulture)
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
