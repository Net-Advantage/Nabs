namespace Nabs.Tests.ReflectionUnitTests.TestClasses;

public class TestClass
{
	private readonly string _name = string.Empty;

	public TestClass()
	{
	}

	public TestClass(string name)
	{
		_name = name;
	}

	public bool DoStuffDone { get; set; }
	public bool DoStuffAsyncDone { get; set; }
	public int Age { get; set; }

	public void DoStuff()
	{
		DoStuffDone = true;
	}

	public async Task DoNothingAsync()
	{
		await Task.CompletedTask;
	}

	public async Task SetAgeAsync(int age)
	{
		Age = age;
		DoStuffAsyncDone = true;
		await Task.CompletedTask;
	}

	public async Task<string> GetNameAsync()
	{
		DoStuffAsyncDone = true;
		return await Task.FromResult(_name);
	}

	public async Task<string> SetAgeAndGetNameAsync(int age)
	{
		Age = age;
		DoStuffAsyncDone = true;
		return await Task.FromResult(_name);
	}

	public async Task<string?> GetNullAsync()
	{
		string? result = null;
		return await Task.FromResult(result);
	}
}