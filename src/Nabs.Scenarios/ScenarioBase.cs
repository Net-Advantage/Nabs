using MediatR;
using Nabs.ActivityFramework;
using Nabs.Projections;

namespace Nabs.Scenarios;

public abstract class ScenarioBase<TRequest, TResponse, TActivityState>
    : Activity<TActivityState>, IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IProjection
    where TResponse : class, IProjection
    where TActivityState : class, IActivityState
{
    protected ScenarioBase(
        IApplicationContext applicationContext)
    {
        ApplicationContext = applicationContext;
    }

    protected ScenarioBase(
        TActivityState activityState, 
        IApplicationContext applicationContext) 
        : base(activityState)
    {
        ApplicationContext = applicationContext;
    }

    public IApplicationContext ApplicationContext { get; }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var result = await InvokeActivity(request);

        Run();

        await Task.CompletedTask;
        return result;
    }

    protected void InitialiseActivityState(TActivityState activityState)
    {
        InitialActivityState = activityState;
        ActivityState = InitialActivityState;
    }

    protected abstract Task<TResponse> InvokeActivity(TRequest request);
}