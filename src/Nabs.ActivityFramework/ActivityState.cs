namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityState
{
    Guid Id { get; set; }
}

public abstract record ActivityState : IActivityState
{
    public Guid Id { get; set; }
}