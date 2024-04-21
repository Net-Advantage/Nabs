
namespace Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario.GetList;

public sealed class PersonList : IListProjection<PersonListItem>
{
    public List<PersonListItem> Items { get; } = [];

}