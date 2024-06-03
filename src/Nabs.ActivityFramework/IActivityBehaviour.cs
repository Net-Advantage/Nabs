namespace Nabs.ActivityFramework;

public interface IActivityStateBehaviour;

public interface IActivityStateBehaviourSync<TActivityState>
    : IActivityStateBehaviour
    where TActivityState : class, IActivityState
{
    TActivityState Run(TActivityState activityState);
}

public interface IActivityStateBehaviourAsync<TActivityState>
    : IActivityStateBehaviour
    where TActivityState : class, IActivityState
{
    Task<TActivityState> RunAsync(TActivityState activityState);
}
