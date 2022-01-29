namespace Nabs.Tests.PersistenceTests.TestModels;

public record TestUser(
        Guid Id,
        string Username,
        string FirstName)
    : IRelationalEntity<Guid>;

