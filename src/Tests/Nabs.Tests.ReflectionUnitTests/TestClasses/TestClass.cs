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

    public bool Completed { get; set; }
    public int Age { get; set; }

    public void DoStuff()
    {
        Completed = true;
    }

    public async Task DoNothingAsync()
    {
        Completed = true;
        await Task.CompletedTask;
    }

    public async Task SetAgeAsync(int age)
    {
        Age = age;
        Completed = true;
        await Task.CompletedTask;
    }

    public async Task<string> GetNameAsync()
    {
        Completed = true;
        return await Task.FromResult(_name);
    }

    public async Task<string> SetAgeAndGetNameAsync(int age)
    {
        Age = age;
        Completed = true;
        return await Task.FromResult(_name);
    }

    public async Task<string?> GetNullAsync()
    {
        string? result = null;
        Completed = true;
        return await Task.FromResult(result);
    }
}