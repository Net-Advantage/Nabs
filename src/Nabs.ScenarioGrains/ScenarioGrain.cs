namespace Nabs.ScenarioGrains;

public interface IScenarioGrain : IAddressable;

public abstract class ScenarioGrain<TGrainState> : Grain, IScenarioGrain
        where TGrainState : class
{
    protected IPersistentState<TGrainState> GrainState { get; }
    protected IGrainRepository<TGrainState> GrainRepository { get; }

    public ScenarioGrain(
        IPersistentState<TGrainState> state,
        IGrainRepository<TGrainState> grainRepository)
    {
        GrainState = state;
        GrainRepository = grainRepository;
    }

    public override async Task OnActivateAsync(
        CancellationToken cancellationToken)
    {
        await GrainState.ReadStateAsync();

        if (!GrainState.RecordExists)
        {
            var queryResult = await GrainRepository.Query(this);

            if (queryResult.IsSuccess)
            {
                GrainState.State = queryResult.Value;
                await GrainState.WriteStateAsync();
            }
            else
            {
                throw new InvalidOperationException(
                    $"Failed to query state for {this.GetPrimaryKey()}");
            }
        }

        await base.OnActivateAsync(cancellationToken);
    }

    public override async Task OnDeactivateAsync(
        DeactivationReason reason,
        CancellationToken cancellationToken)
    {
        await GrainState.WriteStateAsync();

        var persistResult = await GrainRepository.Persist(this, GrainState.State);
        if(persistResult.IsSuccess)
        {
            await GrainState.ClearStateAsync();
        }
        else
        {
            throw new InvalidOperationException(
                $"Failed to persist state for {this.GetPrimaryKey()}");
        }

        await base.OnDeactivateAsync(reason, cancellationToken);
    }
}
