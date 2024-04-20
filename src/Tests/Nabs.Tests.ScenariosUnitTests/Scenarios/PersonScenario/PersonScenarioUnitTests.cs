using Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario.GetList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario;

public sealed class PersonScenarioUnitTests
{
    [Fact]
    public async Task GetPersonListHandler_ShouldReturnPersonListProjection()
    {
        // Arrange
        var applicationContext = new ApplicationContext();
        var personListProjection = new PersonListProjection();
        var getPersonListHandler = new GetPersonListHandler(applicationContext);

        // Act
        var result = await getPersonListHandler.Handle(new GetListRequest(), CancellationToken.None);

        // Assert
        result.Should().BeOfType<PersonListProjection>();
    }
}
