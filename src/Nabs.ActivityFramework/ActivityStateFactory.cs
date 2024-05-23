namespace Nabs.ActivityFramework;

public interface IActivityStateFactory<TActivityState>
    where TActivityState : class, IActivityState
{
    TActivityState Run();
};


/// <summary>
/// Creates a new instance of TActivityState.
/// </summary>
/// <typeparam name="TActivityState"></typeparam>
public abstract class ActivityStateFactory<TActivityState>
    : IActivityStateFactory<TActivityState>
    where TActivityState : class, IActivityState
{
    public abstract TActivityState Run();
}
