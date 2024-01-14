# Test Project Structure

The basis of this library is to address the challenges of enterprise grade testing.

It is always a challenge to write tests that are repeatable, maintainable, and readable.  This library attempts to address these challenges by providing a set of base classes that can be used to create a consistent testing experience.

Since this library assume xUnit as the preferred unit testing framework, we will lean on a fixture based approach to testing.  This means that we will create a fixture class that will be used to setup the test environment and provide the necessary dependencies to the test class.

## The Fixture Types

### `ITestFixture` Interface
The `ITestFixture` interface is designed to standardize the setup process for test fixtures within the Nabs testing framework. This interface ensures consistency and reliability across different test suites by providing a common structure for initializing and disposing of resources required for tests.

### `TestFixtureBase` Abstract Class
The `TestFixtureBase` class, part of the `Nabs.Tests.Fixtures` namespace, serves as an abstract base class for creating test fixtures in the Nabs testing framework. It implements the `ITestFixture` interface, providing foundational setup and teardown functionalities essential for managing test environments. This class introduces a constructor that accepts an `IMessageSink` object, which is used for diagnostic messaging during tests. The `TestFixtureBase` class features a `TestOutputHelper` property, enabling derived classes to output test messages conveniently. Additionally, it includes an OutputLine method to facilitate unified message logging to both the diagnostic message sink and the test output helper, ensuring a streamlined approach to handling test diagnostics and output. The class also adheres to proper resource management practices as indicated in Microsoft's documentation, particularly in its implementation of the `Dispose` method, which is critical for ensuring efficient cleanup of resources post-testing. This class serves as a robust starting point for creating specific test fixtures, offering a structured approach to initializing tests and handling test-related diagnostics effectively.

### `SimpleTestFixture` Class
The `SimpleTestFixture` class, residing in the `Nabs.Tests.Fixtures` namespace, is a concrete implementation of the `TestFixtureBase` class. It is tailored for straightforward test scenarios within the Nabs testing framework. The class is characterized by its simplicity and direct focus on essential test setup functionalities. By inheriting from `TestFixtureBase`, `SimpleTestFixture` leverages the foundational setup, teardown, and diagnostic messaging capabilities already established in the base class. The constructor of `SimpleTestFixture` accepts an IMessageSink object, which is passed to the `TestFixtureBase` constructor, ensuring that diagnostic messaging capabilities are integrated seamlessly. The primary function of this class is embodied in its override of the `Initialise` method. This method is intended to be implemented with specific initialization logic tailored to the needs of the tests it supports. The `SimpleTestFixture` class is ideal for scenarios where a basic, no-frills approach to test setup is desired, providing a clean and efficient way to create and manage test environments with minimal overhead.

## The Test Types

### `FixtureTestBase<TTestFixture>` Abstract Class
The `FixtureTestBase<TTestFixture>` class, located in the `Nabs.Tests` namespace, is an abstract generic class designed to provide a structured foundation for test cases using test fixtures in the Nabs testing framework. It is tailored to work with any test fixture that inherits from `TestFixtureBase`, as indicated by the generic constraint `TTestFixture : TestFixtureBase`.

This class implements the `IAsyncLifetime` interface, enabling asynchronous initialization and disposal patterns, which are essential for modern test scenarios that might involve asynchronous operations. The constructor of `FixtureTestBase<TTestFixture>` initializes the test fixture and assigns a `TestOutputHelper`` to it for logging purposes. It also logs the construction process, enhancing traceability and debugging.

Key features of this class include the `OutputScenario` and `OutputStep` methods, which provide a convenient way to log test scenarios and steps, aiding in the readability and maintainability of test logs.

The `StartTest` and `TeardownTest` methods are virtual and asynchronous, allowing derived classes to implement specific test setup and teardown logic asynchronously. This design is particularly useful for tests that require complex setup or cleanup operations, including those involving IO-bound tasks or external services.

`InitializeAsync` and `DisposeAsync` methods, part of the `IAsyncLifetime` interface implementation, are responsible for the lifecycle management of the test. `InitializeAsync` sets up the test environment using the `TestFixture.Initialise` method and then calls `StartTest`. Conversely, `DisposeAsync` ensures proper teardown of the test environment by calling `TeardownTest`.

The `FixtureTestBase<TTestFixture>` class serves as a versatile and robust foundation for creating various types of test cases, offering flexibility, structured logging, and support for asynchronous operations, thereby facilitating efficient and effective testing practices within the Nabs testing framework.

## The Attribute Types

### `LoadFromCsvDataAttribute<T>` Attribute Class

The `LoadFromCsvDataAttribute<T>` class, part of the `Nabs.Tests` namespace, is a custom attribute designed to facilitate data-driven testing in the Nabs testing framework, specifically for tests that require data loading from CSV files. This attribute is generic, denoted by `<T>`, where T is constrained to be a class type that has a parameterless constructor (`new()` constraint). The class extends DataAttribute, indicating its use in defining data sources for test methods.

The primary role of `LoadFromCsvDataAttribute<T>` is to provide a seamless way to load test data from embedded CSV resources within an assembly. It's adorned with `[AttributeUsage]` to specify that it should be applied to methods only and does not support multiple uses on the same method or inheritance.

Key properties of this attribute include `_relativeAssemblyType`, `_resourceFilePathEndsWith`, `_csvConfiguration`, and `_resourceLoader`. The `_relativeAssemblyType` and `_resourceFilePathEndsWith` are used to locate the CSV file within the assembly. The `_resourceLoader` is responsible for loading the embedded resource, and `_csvConfiguration` configures how the CSV data is parsed, including delimiter settings and handling of header records.

The core functionality of this attribute is implemented in the `GetData` method. This method overrides the abstract method from `DataAttribute` and is responsible for fetching the data from the specified CSV resource. It utilizes a CSV parser to convert the contents of the CSV file into a collection of objects of type T. This collection is then provided to the test method as test data.

The `LoadFromCsvDataAttribute<T>` class is particularly useful for parameterized tests where each test case can be driven by a row of data from a CSV file. This approach enhances the reusability and scalability of test cases, allowing for a broad range of input scenarios to be tested efficiently.

### `LoadEnumerableFromJsonDataAttribute<T>` Attribute Class
