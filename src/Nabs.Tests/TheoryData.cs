using System.Collections;

namespace Nabs.Tests;

public abstract class BaseTheoryData : IEnumerable<object[]>
{
	readonly List<object[]> _data = new();

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