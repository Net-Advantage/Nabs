namespace Nabs.Tests.ScenariosUnitTests;

public sealed class TenantEntityUnitTest
{
    [Fact]
    public void CreateTenantId_Success()
    {
        // Arrange

        // Act
        var tenantId = TenantId.Create(Guid.NewGuid());

        // Assert
        tenantId.Should().NotBeNull();
    }


    [Fact]
    public void CreateTenantId_Failure()
    {
        // Arrange

        // Act
        var action = () => TenantId.Create(Guid.Empty);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CompareTenantByDifferentId_Success()
    {
        // Arrange
        var tenantId1 = TenantId.Create(Guid.NewGuid());
        var tenantId2 = TenantId.Create(Guid.NewGuid());

        // Act
        var resultOperators = tenantId1 == tenantId2;
        var resultEquals = tenantId1.Equals(tenantId2);

        // Assert
        resultOperators.Should().BeFalse();
        resultEquals.Should().BeFalse();
    }

    [Fact]
    public void CompareTenantBySameId_Success()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId1 = TenantId.Create(id);
        var tenantId2 = TenantId.Create(id);

        // Act
        var resultOperators = tenantId1 == tenantId2;
        var resultEquals = tenantId1.Equals(tenantId2);

        // Assert
        resultOperators.Should().BeTrue();
        resultEquals.Should().BeTrue();
    }

    [Fact]
    public void CompareTenantByInstance_Success()
    {
        // Arrange
        var tenantId1 = TenantId.Create(Guid.NewGuid());
        var tenantId2 = tenantId1;

        // Act
        // Act
        var resultOperators = tenantId1 == tenantId2;
        var resultEquals = tenantId1.Equals(tenantId2);

        // Assert
        resultOperators.Should().BeTrue();
        resultEquals.Should().BeTrue();
    }

    [Fact]
    public void CompareTenantByInstanceAndNull_Success()
    {
        // Arrange
        var tenantId1 = TenantId.Create(Guid.NewGuid());
        TenantId? tenantId2 = null;

        // Act
        var resultOperators = tenantId1 == tenantId2;
        var resultEquals = tenantId1.Equals(tenantId2);

        // Assert
        resultOperators.Should().BeFalse();
        resultEquals.Should().BeFalse();
    }

    [Fact]
    public void CompareTenantByNullAndInstance_Success()
    {
        // Arrange
        TenantId? tenantId1 = null;
        var tenantId2 = TenantId.Create(Guid.NewGuid());

        // Act
        var resultOperators = tenantId1 == tenantId2;

        // Assert
        resultOperators.Should().BeFalse();
    }

    [Fact]
    public void CompareTenantByNullAndNull_Success()
    {
        // Arrange
        TenantId? tenantId1 = null;
        TenantId? tenantId2 = null;

        // Act
        var resultOperators = tenantId1 == tenantId2;

        // Assert
        resultOperators.Should().BeTrue();
    }

    [Fact]
    public void CompareTenantNot_Success()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId1 = TenantId.Create(id);
        var tenantId2 = TenantId.Create(id);

        // Act
        var resultOperators = tenantId1 != tenantId2;

        // Assert
        resultOperators.Should().BeFalse();
    }

    [Fact]
    public void CompareTenantToOtherType_Success()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId1 = TenantId.Create(id);
        var otherType = new object();

        // Act
        var resultOperators = tenantId1.Equals(otherType);

        // Assert
        resultOperators.Should().BeFalse();
    }

    [Fact]
    public void GetEqualityComponents_ReturnsAllComponents()
    {
        // Arrange
        var tenantId = TenantId.Create(Guid.NewGuid());

        // Act
        var components = tenantId.GetEqualityComponents().ToList();

        // Assert
        components.Should().HaveCount(1);
        components.Should().Contain(tenantId.Id);
    }

    [Fact]
    public void GetHashCode_Success()
    {
        // Arrange
        var tenantId = TenantId.Create(Guid.NewGuid());

        // Act
        var hashCode = tenantId.GetHashCode();

        // Assert
        hashCode.Should().NotBe(0);
    }
}
