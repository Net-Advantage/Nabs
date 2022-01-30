using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Nabs.Tests
{
    public abstract class TestBase : IAsyncLifetime
    {
        private readonly TextWriter _originalOut;
        private readonly TextWriter _textWriter;

        protected TestBase([NotNull]TestFixtureBase testFixture, [NotNull]ITestOutputHelper output)
        {
            TestFixture = testFixture;
            Output = output;

            _originalOut = Console.Out;
            _textWriter = new StringWriter();
            Console.SetOut(_textWriter);

            if (testFixture != null)
            {
                ConfigurationRoot = testFixture.ConfigurationRoot;
                ServiceProvider = testFixture.ServiceScope.ServiceProvider;
            }
        }

        protected TestFixtureBase TestFixture { get; }
        protected ITestOutputHelper Output { get; }
        protected IServiceProvider ServiceProvider { get; }
        protected IConfigurationRoot ConfigurationRoot { get; }

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
            Output.WriteLine("InitializeAsync");
            await StartTest();
        }

        public async Task DisposeAsync()
        {
            Output.WriteLine(_textWriter.ToString());

            Output.WriteLine("DisposeAsync");
            await TeardownTest();
            Console.SetOut(_originalOut);
        }
    }
}