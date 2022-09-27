using System.IO;

namespace Nabs.Tests;

public abstract class TestBase<TTestFixture> : TestBase
	where TTestFixture : TestFixtureBase, new()
{
	protected TestBase(
		TTestFixture testFixture,
		ITestOutputHelper output) : base(output)
	{
		TestFixture = testFixture;
	}

	protected TTestFixture TestFixture { get; }

	public virtual async Task StartTest()
	{
		await Task.CompletedTask;
	}

	public virtual async Task TeardownTest()
	{
		await Task.CompletedTask;
	}

	public async Task InitializeAsync()
	{
		Output.WriteLine("Test is initialising");
		await StartTest();
	}

	public async Task DisposeAsync()
	{
		Output.WriteLine(TextWriter.ToString());

		await TeardownTest();

		Output.WriteLine("Test has ended");
	}

}