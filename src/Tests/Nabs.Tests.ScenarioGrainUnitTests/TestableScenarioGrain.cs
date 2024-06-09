namespace Nabs.Tests.ScenarioGrainUnitTests;

public record TestableGrainState(
    string StringValue);

public sealed class TestableScenarioGrain : ScenarioGrain<TestableGrainState>
{
    public TestableScenarioGrain(
        IPersistentState<TestableGrainState> state,
        IGrainRepository<TestableGrainState> grainQueryStrategy)
        : base(state, grainQueryStrategy)
    {
    }
}
