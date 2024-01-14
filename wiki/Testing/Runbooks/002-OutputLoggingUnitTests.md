# Output Logging Unit Test Runbook

All tests should provide enough information to the observer to validate that the necessary steps have been executed. This is achieved by providing outputs that will be displayed in the ___Test Details Summary___ window of the test runner in the _Visual Studio IDE_.

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
