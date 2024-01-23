namespace Nabs.ActivityFramework;

public interface IWorkflowRepository<TRepositoryParameters, TWorkflowState>
    where TRepositoryParameters : class, IWorkflowParameters
    where TWorkflowState : class, IWorkflowState
{
    Task<TWorkflowState> Load(TRepositoryParameters parameters);
    Task Persist(TWorkflowState workflowState);
}
