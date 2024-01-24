namespace Nabs.ActivityFramework;

public interface IWorkflowRepository<TRepositoryParameters, TWorkflowState>
	where TRepositoryParameters : class, IWorkflowParameters
	where TWorkflowState : class, IWorkflowState
{
	Task<Result<TWorkflowState>> LoadAsync(TRepositoryParameters parameters);
	Task<Result<bool>> PersistAsync(TWorkflowState workflowState);
}
