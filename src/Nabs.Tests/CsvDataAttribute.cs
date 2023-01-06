namespace Nabs.Tests;

public class CsvDataAttribute<T> : BaseDataAttribute<T>
	where T : class, new()
{
	public override IEnumerable<T> GetData(MethodInfo testMethod)
	{
		for (int i = 0; i < 10; i++)
		{
			yield return new T();
		}
	}
}