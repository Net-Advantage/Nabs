using Nabs.Resources;

namespace Nabs.Tests;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class CsvDataAttribute<T> : DataAttribute
	where T : class, new()
{
	private readonly Type _relativeAssemblyType;
	private readonly string _resourceFilePath;
	private readonly ResourceLoader _resourceLoader;

	public CsvDataAttribute(Type relativeAssemblyType, string resourceFilePath)
	{
		_relativeAssemblyType = relativeAssemblyType;
		_resourceFilePath = resourceFilePath;
		_resourceLoader = new ResourceLoader(_relativeAssemblyType);
	}

	public override IEnumerable<object[]> GetData(MethodInfo testMethod)
	{
		throw new NotImplementedException();
	}

	//public override IEnumerable<T> GetData(MethodInfo testMethod)
	//{
	//	for (int i = 0; i < 10; i++)
	//	{
	//		yield return new T();
	//	}
	//}
}