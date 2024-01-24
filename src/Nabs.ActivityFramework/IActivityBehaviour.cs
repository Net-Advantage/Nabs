namespace Nabs.ActivityFramework;

public interface IActivityStateBehaviour<TActivityState>
	where TActivityState : class, IActivityState
{
	Task<TActivityState> RunAsync(TActivityState activityState);
}
