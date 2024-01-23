namespace Nabs.ActivityFramework;

public interface IActivityState
{
    Guid Id { get; set; }
}

public abstract record ActivityState : IActivityState
{
    public Guid Id { get; set; }
}