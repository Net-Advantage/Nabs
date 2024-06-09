namespace Nabs.ActivityFramework;

public interface IActivity
{
    bool HasStateChanged { get; }
    ValidationResult ValidationResult { get; }
    Task RunAsync();
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
public abstract class Activity<TActivityState>
    : IActivity<TActivityState>
    where TActivityState : class, IActivityState
{
    private IActivityStateFactory<TActivityState>? _activityStateFactory;
    private IActivityStateValidator<TActivityState>? _activityStateValidator;

    protected Activity()
    {
    }

    protected Activity(TActivityState activityState)
    {
        InitialActivityState = activityState;
        ActivityState = activityState;
    }

    private TActivityState? _initialActivityState;
    public TActivityState? InitialActivityState
    {
        get { return _initialActivityState; }
        protected set
        {
            if (_initialActivityState is not null)
            {
                throw new InvalidOperationException("InitialActivityState can only be set once.");
            }
            _initialActivityState = value;
            ActivityState = value;
        }
    }
    public TActivityState? ActivityState { get; protected set; } = default!;

    public bool HasStateChanged => InitialActivityState != ActivityState;

    public ValidationResult ValidationResult { get; set; } = default!;

    protected Dictionary<IActivityStateBehaviour, Action?> Behaviours { get; } = [];

    protected void AddFactory(IActivityStateFactory<TActivityState> factory)
    {
        _activityStateFactory = factory;
    }

    protected void AddValidator(IActivityStateValidator<TActivityState> validator)
    {
        _activityStateValidator = validator;
    }

    protected void AddBehaviour(IActivityStateBehaviour behaviour, Action? action = null)
    {
        Behaviours.Add(behaviour, action);
    }

    public async Task RunAsync()
    {
        if (InitialActivityState is null)
        {
            if (_activityStateFactory is not null)
            {
                InitialActivityState = _activityStateFactory.Run();
            }
            else
            {
                throw new InvalidOperationException("InitialActivityState is null and no factory has been provided. Set the InitialActivityState manually.");
            }
        }

        ActivityState ??= InitialActivityState;

        if (Behaviours.Count > 0)
        {
            await ProcessBehaviours();
        }

        if (_activityStateValidator is not null)
        {
            ValidationResult = _activityStateValidator.Run(ActivityState);
        }
        ValidationResult ??= new ValidationResult();
    }

    private async Task ProcessBehaviours()
    {
        if (ActivityState is null)
        {
            return;
        }

        foreach (var behaviour in Behaviours)
        {
            if (behaviour.Key is IActivityStateBehaviourSync<TActivityState> syncBehaviour)
            {
                ActivityState = syncBehaviour.Run(ActivityState);

            }
            else if (behaviour.Key is IActivityStateBehaviourAsync<TActivityState> asyncBehaviour)
            {
                ActivityState = await asyncBehaviour.RunAsync(ActivityState);
            }
            else
            {
                throw new InvalidOperationException("Behaviour is not of type IActivityStateBehaviourSync or IActivityStateBehaviourAsync.");
            }

            if (behaviour.Value is not null)
            {
                behaviour.Value();
            }
        }
    }
}