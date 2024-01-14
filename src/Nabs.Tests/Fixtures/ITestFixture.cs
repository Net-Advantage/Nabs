namespace Nabs.Tests.Fixtures;

public interface ITestFixture : IDisposable
{
	public void Initialise();
}
