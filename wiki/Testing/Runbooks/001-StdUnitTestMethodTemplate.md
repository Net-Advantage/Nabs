# Standard Unit Test Method Template Runbook

All tests should follow a basic convention. We recommend that each test method is broken down into the following basic parts.
- Arrange - set up the variables for the inputs to the test.
- Act - execute the code your are targeting for the test.
- Assert - examine the results

Example:
```csharp
[Fact]
[ExcludeFromCodeCoverage]
public void Multiply_A_and_B__Success()
{
    //Arrange
    const int a = 2;
    const int b = 2;
    const int expected = 4;
    
    //Act
    const int actual = MathUtils.Multiply(a, b);

    //Assert
    actual.Should().Be(expected);
}
```

