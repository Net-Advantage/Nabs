using System.Threading.Tasks;

namespace Nabs.Tests.NabsUnitTests.TestClasses
{
	public class TestClass
	{
		public bool DoStuffDone { get; set; }
		public bool DoStuffAsyncDone { get; set; }

		public void DoStuff()
		{
			DoStuffDone = true;
		}

		public async Task DoStuffAsync()
		{
			DoStuffAsyncDone = true;
			await Task.CompletedTask;
		}
	}
}