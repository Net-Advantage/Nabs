namespace Nabs.Tests.NabsUnitTests;

public sealed class TestValueObjectTests
{
    [Fact]
    public void GetHashCode_WithNullProperty_CoversNullPath()
    {
        // Arrange
        var objWithNull = new TestValueObject(null);
        var objWithNonNull = new TestValueObject("Test");

        // Act
        var hashCodeWithNull = objWithNull.GetHashCode();
        var hashCodeWithNonNull = objWithNonNull.GetHashCode();

        // Assert
        Assert.NotEqual(hashCodeWithNull, hashCodeWithNonNull);
    }
}