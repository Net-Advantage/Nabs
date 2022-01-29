using System.Diagnostics.CodeAnalysis;

namespace Nabs.Tests
{
    public abstract class TestBase : IAsyncLifetime
    {
        protected TestBase([NotNull]TestFixtureBase testFixture, [NotNull]ITestOutputHelper output)
        {
            TestFixture = testFixture;
            Output = output;
            ConfigurationRoot = testFixture.ConfigurationRoot;
            ServiceProvider = testFixture.ServiceScope.ServiceProvider;
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
            Output.WriteLine("DisposeAsync");
            await TeardownTest();
        }
    }
}