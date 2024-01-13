namespace Nabs.Tests;

public class ScenarioTheoryData<T>
	where T : class, new()
{
	public string Scenario { get; set; } = default!;
	public T Data { get; set; } = default!;
}