namespace Nabs.Tests;

public abstract class TestBase
{
	private readonly TextWriter _originalOut;
	private readonly TextWriter _textWriter;

	protected TestBase(
		ITestOutputHelper output)
	{
		Output = output;
		Output.WriteLine("Test is starting");

		_originalOut = Console.Out;
		_textWriter = new StringWriter();
		Console.SetOut(_textWriter);
	}

	protected ITestOutputHelper Output { get; }
	protected IServiceProvider ServiceProvider { get; init; }
	protected IConfigurationRoot ConfigurationRoot { get; init; }

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
		Output.WriteLine(_textWriter.ToString());

		await TeardownTest();
		Console.SetOut(_originalOut);

		Output.WriteLine("Test has ended");
	}
}

public abstract class TestBase<TTestFixture> : TestBase
	where TTestFixture : TestFixtureBase, new()
{
	protected TestBase(
		TTestFixture testFixture,
		ITestOutputHelper output) : base(output)
	{
		TestFixture = testFixture;
		ConfigurationRoot = testFixture.ConfigurationRoot;
		ServiceProvider = testFixture.ServiceScope.ServiceProvider;
	}

	protected TTestFixture TestFixture { get; }

}