namespace Nabs.Tests;

public class ScenarioWrapper<T>
{
	public int Scenario { get; set; }
	public int ScenarioName { get; set; }
	public T? Data { get; set; }

	public bool ValidationResult { get; set; }
	public string? ExpectedWarnings { get; set; }
	public string? ExpectedErrors { get; set; }
}