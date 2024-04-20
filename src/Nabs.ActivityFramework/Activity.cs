namespace Nabs.ActivityFramework;

public interface IActivity
{
    bool HasStateChanged { get; }
    ValidationResult ValidationResult { get; }
    void Run();
}

public interface IActivity<TActivityState>
    : IActivity
    where TActivityState : class, IActivityState;

/// <summary>
/// An activity that holds the state based on TActivityState.
/// An activity that simply creates and instance of the state defined by TActivityState.
/// Typically this state is used to start off a workflow that will present blank or partially filled state.
/// </summary>
/// <typeparam name="TActivityState"></typeparam>
public abstract class Activity<
    TActivityState>
    : IActivity<TActivityState>
    where TActivityState : class, IActivityState
{
    private IActivityStateFactory<TActivityState>? _activityStateFactory;
    private IActivityStateValidator<TActivityState>? _activityStateValidator;

    protected Activity(TActivityState activityState)
    {
        InitialActivityState = activityState;
        ActivityState = activityState;
    }

    public TActivityState? InitialActivityState { get; protected set; }
    public TActivityState? ActivityState { get; protected set; } = default!;

    public bool HasStateChanged => InitialActivityState != ActivityState;

    public ValidationResult ValidationResult { get; set; } = default!;

    protected Dictionary<IActivityStateBehaviour<TActivityState>, Action?> Behaviours { get; } = [];

    protected void AddFactory(IActivityStateFactory<TActivityState> factory)
    {
        _activityStateFactory = factory;
    }

    protected void AddValidator(IActivityStateValidator<TActivityState> validator)
    {
        _activityStateValidator = validator;
    }

    protected void AddBehaviour(IActivityStateBehaviour<TActivityState> behaviour, Action? action = null)
    {
        Behaviours.Add(behaviour, action);
    }

    public virtual void Run()
    {
        if (InitialActivityState is null 
            && _activityStateFactory is not null)
        {
            InitialActivityState = _activityStateFactory.Run();
        }

        ActivityState ??= InitialActivityState;

        if (Behaviours.Count > 0)
        {
            ProcessBehaviours();
        }

        if (_activityStateValidator is not null)
        {
            ValidationResult = _activityStateValidator.Run(ActivityState);
        }
        ValidationResult ??= new ValidationResult();
    }

    public virtual void ProcessBehaviours()
    {
        if (ActivityState is null)
        {
            return;
        }

        foreach (var behaviour in Behaviours)
        {
            ActivityState = behaviour.Key.Run(ActivityState);
            if (behaviour.Value is not null)
            {
                behaviour.Value();
            }
        }
    }
}