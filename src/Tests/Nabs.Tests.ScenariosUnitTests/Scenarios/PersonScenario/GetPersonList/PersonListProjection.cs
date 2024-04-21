using Nabs.Projections;

namespace Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario.GetList;

public sealed class PersonListProjection : IListProjection<PersonListItemProjection>
{
    public IEnumerable<PersonListItemProjection> Items { get; } = [];
}