using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nabs.Tests
{
    public abstract class TestBase : IAsyncLifetime
    {
        protected TestBase(ITestOutputHelper output)
        {
            Output = output;
        }

        protected ITestOutputHelper Output { get; }

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