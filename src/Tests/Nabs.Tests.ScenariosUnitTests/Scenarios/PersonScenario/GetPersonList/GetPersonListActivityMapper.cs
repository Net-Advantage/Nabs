
namespace Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario.GetList;

internal class GetPersonListActivityMapper
    : ActivityStateTransformer<GetPersonListActivityState>
{
    public override GetPersonListActivityState Run(GetPersonListActivityState activityState)
    {
        var result = activityState with
        {
            Result = new PersonList()
        };

        var items = result.PersonEntities.Select(i => new PersonListItem
        {
            Id = i.Id,
            FullName = $"{i.FirstName} {i.LastName}"
        }).ToList();

        result.Result.Items.AddRange(items);
        return result;
    }
}
