using Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario.GetList;

namespace Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario;

public sealed class PersonScenarioUnitTests
{
    [Fact]
    public async Task GetPersonListHandler_ShouldReturnPersonListProjection()
    {
        // Arrange
        var applicationContext = new ApplicationContext();
        var getPersonListScenario = new GetPersonListScenario(applicationContext);

        // Act
        var result = await getPersonListScenario.Handle(new GetListRequest(), CancellationToken.None);

        // Assert
        result.Should().BeOfType<GetListResponse>();
    }
}
