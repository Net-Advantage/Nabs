namespace Nabs.Tests;

public class ScenarioTheoryData<T>
	where T : class, new()
{
	public string Scenario { get; set; }
	public T Data { get; set; }
}