namespace Nabs.ScenarioGrains;

public interface IScenarioGrain : IAddressable;

public abstract class ScenarioGrain<TGrainState>(
    IPersistentState<TGrainState> state,
    IGrainRepository<TGrainState> grainRepository)
        : Grain, IScenarioGrain
        where TGrainState : class
{
    protected IPersistentState<TGrainState> GrainState { get; } = state;
    protected IGrainRepository<TGrainState> GrainRepository { get; } = grainRepository;

    public override async Task OnActivateAsync(
        CancellationToken cancellationToken)
    {
        await GrainState.ReadStateAsync();

        if (!GrainState.RecordExists)
        {
            await GrainRepository.Query(this);

            await GrainState.WriteStateAsync();
        }

        await base.OnActivateAsync(cancellationToken);
    }

    public override async Task OnDeactivateAsync(
        DeactivationReason reason,
        CancellationToken cancellationToken)
    {
        await GrainState.WriteStateAsync();

        await GrainRepository.Persist(this, GrainState.State);

        await base.OnDeactivateAsync(reason, cancellationToken);
    }
}
