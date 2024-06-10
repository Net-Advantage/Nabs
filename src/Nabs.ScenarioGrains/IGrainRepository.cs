namespace Nabs.ScenarioGrains;

public interface IGrainRepository<TGrainState>
{
    Task<Result<TGrainState>> Query(IScenarioGrain grain);
    Task<Result> Persist(IScenarioGrain grain, TGrainState state);
}