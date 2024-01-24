namespace Nabs.Tests.TestDatabaseTests.EntityRepositoryTests;

public sealed class GetFirstAllTypesDtoSpecification 
	: Specification<AllTypesEntity, AllTypesDto>
{
	public GetFirstAllTypesDtoSpecification()
	{
		Query
			.Where(x => x.StringColumn == "First");

		Query
			.Select(x => new AllTypesDto
			{
				StringColumn = x.StringColumn
			});
	}
}
