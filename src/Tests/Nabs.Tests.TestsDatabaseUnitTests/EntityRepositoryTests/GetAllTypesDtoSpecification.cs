﻿using Nabs.Tests.TestsDatabaseUnitTests.Database;
using Nabs.Tests.TestsDatabaseUnitTests.EntityRepositoryTests;

namespace Nabs.Tests.TestDatabaseUnitTests.EntityRepositoryTests;

public sealed class GetAllTypesDtoSpecification
    : Specification<AllTypesEntity, AllTypesDto>
{
    public GetAllTypesDtoSpecification(string stringColumnValue)
    {
        Query
            .Where(x => x.StringColumn == stringColumnValue);

        Query
            .Select(x => new AllTypesDto
            {
                StringColumn = x.StringColumn
            });
    }
}
