namespace Nabs.ActivityFramework;

public interface IWorkflowState
{
	Guid Id { get; set; }
}

public abstract class WorkflowState : IWorkflowState
{
	public Guid Id { get; set; }
}
