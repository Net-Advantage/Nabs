namespace Nabs.Tests.ScenarioGrainUnitTests;

public class ScenarioGrainUnitTest
{
    [Fact]
    public async Task ScenarioGrainOnActivate_NoStateExists()
    {
        // Arrange
        var mockState = new Mock<IPersistentState<TestableGrainState>>();
        mockState.Setup(x => x.RecordExists).Returns(false);
        mockState.Setup(x => x.ReadStateAsync()).Returns(Task.CompletedTask);

        var mockQueryStrategy = new Mock<IGrainRepository<TestableGrainState>>();
        mockQueryStrategy.Setup(x => x.Query(It.IsAny<IScenarioGrain>()))
            .ReturnsAsync(new TestableGrainState("String value!"));

        var grain = new TestableScenarioGrain(mockState.Object, mockQueryStrategy.Object);

        // Act
        await grain.OnActivateAsync(CancellationToken.None);

        // Assert
        mockState.Verify(x => x.ReadStateAsync(), Times.Once);
        mockQueryStrategy.Verify(x => x.Query(It.IsAny<IScenarioGrain>()), Times.Once);
        mockState.Verify(x => x.WriteStateAsync(), Times.Once);
    }

    [Fact]
    public async Task ScenarioGrainOnDeactivate_NoStateExists()
    {
        // Arrange
        var testableGrainState = new TestableGrainState("String value!");

        var mockState = new Mock<IPersistentState<TestableGrainState>>();
        mockState.Setup(x => x.RecordExists).Returns(false);
        mockState.Setup(x => x.ReadStateAsync()).Returns(Task.CompletedTask);

        var mockGrainRepository = new Mock<IGrainRepository<TestableGrainState>>();
        mockGrainRepository.Setup(x => x.Query(It.IsAny<IScenarioGrain>()))
            .ReturnsAsync(testableGrainState);

        var grain = new TestableScenarioGrain(mockState.Object, mockGrainRepository.Object);
        var deactivationReason = new DeactivationReason();

        // Act
        await grain.OnDeactivateAsync(deactivationReason, CancellationToken.None);

        // Assert
        mockState.Verify(x => x.ReadStateAsync(), Times.Never);
        mockState.Verify(x => x.WriteStateAsync(), Times.Once);
        mockGrainRepository.Verify(x => x.Persist(It.IsAny<IScenarioGrain>(), It.IsAny<TestableGrainState>()), Times.Once);
    }
}
