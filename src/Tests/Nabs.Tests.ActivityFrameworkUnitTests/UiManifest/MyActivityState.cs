namespace Nabs.Tests.ActivityFrameworkUnitTests.UiManifest;

public class MyActivityState : IActivityState
{
	public Guid Id { get; set; }
	public string Username { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
}
