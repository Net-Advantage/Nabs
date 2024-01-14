using System.Text.Json;

namespace Nabs.Tests;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class LoadEnumerableFromJsonDataAttribute<T> : DataAttribute
	where T : notnull, new()
{
	private readonly Type _relativeAssemblyType;
	private readonly string _resourceFilePathEndsWith;
	private readonly EmbeddedResourceLoader _resourceLoader;
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public LoadEnumerableFromJsonDataAttribute(Type relativeAssemblyType, string resourceFilePathEndsWith)
	{
		_relativeAssemblyType = relativeAssemblyType;
		_resourceFilePathEndsWith = resourceFilePathEndsWith;
		_resourceLoader = new EmbeddedResourceLoader(_relativeAssemblyType);
		_jsonSerializerOptions = CommonTestDependencies.JsonSerializerOptions;
	}

	public override IEnumerable<object[]> GetData(MethodInfo testMethod)
	{
		var items = _resourceLoader.GetResourceStreamContent(x => x.Path.EndsWith(_resourceFilePathEndsWith))
			.Match(
				content =>
				{
					var streamReader = new StreamReader(content);
					var records = JsonSerializer.Deserialize<T[]>(streamReader.ReadToEnd(), _jsonSerializerOptions)!;
					var data = records.Select(x => new object[] { x });
					return data;
				},
				exception =>
				{
					throw exception;
				});

		foreach (var item in items)
		{
			yield return item;
		}
	}
}