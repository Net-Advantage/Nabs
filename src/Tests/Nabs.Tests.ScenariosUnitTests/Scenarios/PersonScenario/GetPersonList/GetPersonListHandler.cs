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
