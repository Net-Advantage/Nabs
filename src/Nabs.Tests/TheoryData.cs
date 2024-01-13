using System.Collections;

namespace Nabs.Tests;

public class TheoryData<T> : TheoryData
	where T : notnull
{
	public void Add(string scenario, T data)
	{
		AddRow(scenario, data);
	}
}

public abstract class TheoryData : IEnumerable<object[]>
{
	readonly List<object[]> _data = [];

	protected void AddRow(params object[] values)
	{
		_data.Add(values);
	}

	public IEnumerator<object[]> GetEnumerator()
	{
		return _data.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}