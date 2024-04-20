namespace Nabs.ActivityFramework;

public interface IActivityStateBehaviour<TActivityState>
	where TActivityState : class, IActivityState
{
	TActivityState Run(TActivityState activityState);
}
