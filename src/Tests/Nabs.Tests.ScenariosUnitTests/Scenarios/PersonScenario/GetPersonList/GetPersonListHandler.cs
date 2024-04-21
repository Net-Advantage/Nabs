using Nabs.Projections;

namespace Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario.GetList;

public sealed class GetPersonListHandler : IRequestHandler<GetListRequest, PersonListProjection>
{
    private readonly IApplicationContext _applicationContext;

    public GetPersonListHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<PersonListProjection> Handle(GetListRequest request, CancellationToken cancellationToken)
    {
        var result = new PersonListProjection();

        // IO BL pipeline

        return await Task.FromResult(result);
    }
}

public sealed class GetListRequest : IRequest<PersonListProjection>;

public abstract class ScenarioBase<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IProjection
    where TResponse : class, IProjection
{
    public abstract Task<TResponse> Handle(
        TRequest request, CancellationToken cancellationToken);

    public void MapRequestToState(TRequest request)
    {
        // Map request to state
    }

    public TResponse MapStateToResponse()
    {
        // Map state to response
    }
}