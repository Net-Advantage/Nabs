using Nabs.ActivityFramework;
using Nabs.Serialisation;
using Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario.GetList;
using Xunit.Abstractions;

namespace Nabs.Tests.ScenariosUnitTests.Scenarios.PersonScenario;

public sealed class PersonScenarioUnitTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public PersonScenarioUnitTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GetPersonListHandler_ShouldReturnPersonListProjection()
    {
        // Arrange
        var applicationContext = new ApplicationContext();
        var getPersonListScenario = new GetPersonListScenario(applicationContext);

        // Act
        var result = await getPersonListScenario
            .Handle(new GetListRequest(), CancellationToken.None);

        // Assert
        result.Should().BeOfType<GetListResponse>();

        var json = DefaultJsonSerializer.Serialize(result);
        _testOutputHelper.WriteLine(json);
    }
}
