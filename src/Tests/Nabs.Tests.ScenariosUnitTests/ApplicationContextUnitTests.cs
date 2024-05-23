namespace Nabs.Tests.ScenariosUnitTests;

public sealed class ApplicationContextUnitTests
{
    [Fact]
    public void CreateApplicationContext_Success()
    {
        // Arrange

        // Act
        var applicationContext = new ApplicationContext();

        // Assert
        applicationContext.Should().NotBeNull();
        applicationContext.TenantContext.Should().NotBeNull();
        applicationContext.UserContext.Should().NotBeNull();
        applicationContext.TenantIsolationStrategy.Should().Be(TenantIsolationStrategy.SharedShared);
        applicationContext.IsTenant(Guid.Empty).Should().BeTrue();
    }
}
