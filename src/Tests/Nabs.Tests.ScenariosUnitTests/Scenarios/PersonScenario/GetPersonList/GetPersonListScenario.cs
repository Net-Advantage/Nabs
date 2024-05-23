namespace Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario.GetList;

public sealed class GetPersonListScenario
    : ScenarioBase<GetListRequest, GetListResponse, GetPersonListActivityState>
{
    public GetPersonListScenario(
        IApplicationContext applicationContext
        //, TechTrekDbContext dbContext
        ) : base(applicationContext)
    {
        AddBehaviour(new GetPersonListActivityMapper());
    }

    protected override async Task InvokeActivity(GetListRequest request)
    {
        var personEntities = await LoadData();

        InitialActivityState = new GetPersonListActivityState(request, personEntities);
    }

    protected override GetListResponse ProcessResult()
    {
        var result = new GetListResponse()
        {
            PersonList = ActivityState!.Result!
        };
        return result;
    }

    private static async Task<List<PersonEntity>> LoadData()
    {
        // This will typically come from the dbContext
        var personEntities = new List<PersonEntity>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Username = "johndoe",
                FirstName = "John",
                LastName = "Doe"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Username = "janedoe",
                FirstName = "Jane",
                LastName = "Doe"
            }
        };
        await Task.CompletedTask;
        return personEntities;
    }
}

public sealed class GetListRequest : IRequest<GetListResponse>, IProjection;
public sealed class GetListResponse : IProjection
{
    public PersonList PersonList { get; set; } = default!;
}