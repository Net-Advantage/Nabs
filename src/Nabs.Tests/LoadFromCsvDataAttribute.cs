namespace Nabs.Tests;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class LoadFromCsvDataAttribute<T> : DataAttribute
	where T : class, new()
{
	private readonly Type _relativeAssemblyType;
	private readonly string _resourceFilePathEndsWith;
	private readonly CsvConfiguration _csvConfiguration;
	private readonly EmbeddedResourceLoader _resourceLoader;

	public LoadFromCsvDataAttribute(Type relativeAssemblyType, string resourceFilePathEndsWith)
	{
		_relativeAssemblyType = relativeAssemblyType;
		_resourceFilePathEndsWith = resourceFilePathEndsWith;
		_resourceLoader = new EmbeddedResourceLoader(_relativeAssemblyType);
		_csvConfiguration = GlobalSettings.CsvConfiguration;
	}

	public override IEnumerable<object[]> GetData(MethodInfo testMethod)
	{
		var items = _resourceLoader.GetResourceStreamContent(x => x.Path.EndsWith(_resourceFilePathEndsWith))
			.Match(
				content =>
				{
					var csvReader = new CsvReader(new StreamReader(content), _csvConfiguration);
					var records = csvReader.GetRecords<T>();
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
