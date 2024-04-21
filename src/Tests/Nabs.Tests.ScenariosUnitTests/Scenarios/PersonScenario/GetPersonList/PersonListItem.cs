namespace Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario.GetList;

public class PersonListItem : IListItemProjection
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
}