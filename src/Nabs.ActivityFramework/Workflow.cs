namespace Nabs.ActivityFramework;

public abstract class Workflow<TWorkflowState>
    where TWorkflowState : class, IWorkflowState
{
    public TWorkflowState? WorkflowState { get; set; }
    public Dictionary<IActivity, Delegate?> Activities { get; } = [];

    public bool Processed { get; private set; }

    public ValidationResult ValidationResult
    {
        get
        {
            if (!Processed)
            {
                return null!;
            }

            var allFailures = Activities
                       .Where(x => x.Key.ValidationResult != null)
                       .SelectMany(x => x.Key.ValidationResult.Errors)
                       .ToList();

            return new ValidationResult(allFailures);
        }
    }

    protected void AddActivity<TActivity>(Action<TActivity>? action = null)
        where TActivity : class, IActivity, new()
    {
        var activity = new TActivity();
        Activities.Add(activity, action);
    }

    protected void AddActivity<TActivity>(IActivity activity, Action<TActivity>? action = null)
        where TActivity : class, IActivity
    {
        Activities.Add(activity, action);
    }

    public void Run()
    {
        Processed = false;
        ProcessActivities();
        Processed = true;
    }

    public virtual void ProcessActivities()
    {
        foreach (var activity in Activities)
        {
            activity.Key.Run();
            activity.Value?.DynamicInvoke(activity.Key);
        }
    }
}
