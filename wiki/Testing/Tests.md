# Nabs.UnitTests Playbook

In this playbook we provide guidance that helps you identify the runbook to suit the type of unit test you are about the write. The runbooks we provide will guide teams and individuals through setting up unit tests.

Target Audience
- Testers
- Test leads
- Developers
- Team leads
- Architects
- Analysts

Use the Unit Testing Decision Tree to create a mental model of good unit tests practices.

![Unit Testing Decision Tree](_images/UnitTestsDecisionTree.png "Unit Tests Decision Tree")

There are two basic runbooks that should be employed at all times:
- Standard Unit Test Method Template Runbook
- Output Logging Unit Test Runbook

Working down from the top...

If you need to provide services to your unit test that are best set up through dependency injection or mocks, then add the Service Collection Unit Test Runbook.

You now need to choose between Multi- or Single-scenario Unit Tests Runbooks.

Lastly you should determine if you need to:
1. load test data for each test using the Scenario Data Unit Test Runbook?
1. bootstrap a data store prior to running the test using the Bootstrap Data Unit Test Runbook?

## Standard Unit Test Method Template Runbook

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

## Output Logging Unit Test Runbook

All tests should provide enough information to the observer to validate the the necessary steps have been executed. This is achieved by providing outputs that will be displayed in the ___Test Details Summary___ window of the test runner in the _Visual Studio IDE_.

1. Create a class for your unit tests. E.g. `SimpleTestWithOutputLogginUnitTests.cs`
1. Inherit from `Nabs.Tests.TestBase`.
1. Add you test methods.
1. You can use the `nabsTestFact` code snippet to generate the method stub.
1. Use the following convenience methods to output your messages:
    - `OutputScenario`
    - `OutputStep`

Example:
```csharp
[Fact]
[ExcludeFromCodeCoverage]
public void Outputs__Success()
{
    OutputScenario();
    OutputStep("#1");
    OutputStep("#2");

    //Manually view the Test Details Summary output window for results.
}
```
The _Test Details Summary_ window will display the following output:
```text
Standard Output: 
    [SimpleTestWithOutputLoggingUnitTests] is constructing...
    =========================================================
    [Outputs__Success] - Scenario: default
    Step: #1
    Step: #2
    ===================================================
    [SimpleTestWithOutputLoggingUnitTests] is disposed!
```


## Single-scenario Data Unit Test Runbook

1. Create your test method stub. You can use the `nabsTestFact` code snippet to generate the method stub.
1. Add the xUnit `Fact`Attribute.
1. Implement your test method.

## Multi-scenario Data Unit Test Runbook

1. Create your test method stub. You can use the `nabsTestTheory` code snippet to generate the method stub.
1. Add the xUnit `Theory`Attribute.
1. Implement your test method.

## Scenario Data From CSV Unit Test Runbook


## Scenario Data From Json Unit Test Runbook


## Service Collection Unit Test Runbook


## Bootstrap Data Unit Test Runbook





Providing a structured approach to testing is one of the most valuable aspects to any software development project.

The goal of this library is to provide a starting point for individuals and teams to get their tests consistently setup.

## Classes

### Abstractions
- `TestFixtureBase`
- `TestBase`
- `TestBase<TTestFixture>`

### Implementations
- `AppSettingsTestFixture`

## Notes

### `TestFixtureBase`

Sets up the configuration and service scope for the test class.

Two virtual methods are provided to:
- `ConfigureConfiguration` to add additional configuration providors to to the `IConfigurationBuilder`.
- `ConfigureServices` to add dependencies to the `IServiceCollection`.

We also provide a basic implementation in the form of `AppSettingsTestFixture` which adds `appsettings.json` configuration handling.


### `TestBase`
