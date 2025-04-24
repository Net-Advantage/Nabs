namespace Nabs.Tests;

public sealed class TestCase<TInput, TOutput>
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string WorkItemId { get; init; }
    public required string CallerTypeName { get; init; }
    public required string CallerMemberName { get; init; }
    public required string Description { get; init; }
    public required string ExpectedOutput { get; init; }
    public required TInput Input { get; init; }
    public required TOutput Output { get; init; }
}

public static class TestCaseFactory
{
    public static TestCase<TInput, TOutput> Create<TInput, TOutput>(
        string id,
        string workItemId,
        string name,
        string description,
        string expectedOutput,
        TInput input,
        TOutput output,
        [CallerMemberName] string caller = "")
    {
        var stackTrace = new System.Diagnostics.StackTrace();
        var callerFrame = stackTrace.GetFrame(1)!;
        var callerType = callerFrame.GetMethod()!.DeclaringType;

        return new TestCase<TInput, TOutput>
        {
            Id = id,
            Name = name,
            WorkItemId = workItemId,
            Description = description,
            CallerTypeName = callerType?.Name ?? string.Empty,
            CallerMemberName = caller,
            ExpectedOutput = expectedOutput,
            Input = input,
            Output = output
        };
    }
}