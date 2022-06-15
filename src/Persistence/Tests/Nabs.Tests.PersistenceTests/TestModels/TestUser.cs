namespace Nabs.Tests.PersistenceTests.TestModels;

public record TestUser(
		Guid Id,
		string Username,
		string FirstName,
		string LastName)
	: IRelationalEntity<Guid>;

