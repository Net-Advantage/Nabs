namespace Nabs.Tests.ActivityFrameworkUnitTests.UiManifest;

public record SimpleActivityState(
	Guid Id,
	string Username,
	string FirstName)
	: ActivityState;