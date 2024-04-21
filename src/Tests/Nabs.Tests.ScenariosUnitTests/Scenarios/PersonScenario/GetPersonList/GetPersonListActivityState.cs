namespace Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario.GetList;

public sealed record GetPersonListActivityState(
    GetListRequest Request,
    List<PersonEntity> PersonEntities) : ActivityState
{
    public PersonList? Result { get; set; }
};
