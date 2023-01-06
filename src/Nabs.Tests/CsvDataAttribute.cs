namespace Nabs.Tests;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class CsvDataAttribute<T> : DataAttribute
	where T : class, new()
{
	private readonly Type _relativeAssemblyType;
	private readonly string _resourceFilePath;
	private readonly ResourceFileLoader _resourceFileLoader;

	public CsvDataAttribute(Type relativeAssemblyType, string resourceFilePath)
	{
		_relativeAssemblyType = relativeAssemblyType;
		_resourceFilePath = resourceFilePath;
		_resourceFileLoader = new ResourceFileLoader(_relativeAssemblyType);
	}

	public override IEnumerable<> GetData(MethodInfo testMethod)
	{
		for (int i = 0; i < 10; i++)
		{
			yield return new T();
		}
	}
}