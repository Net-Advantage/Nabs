namespace Nabs.Tests.ActivityFrameworkUnitTests.Activities;

public sealed class ActivityOfTStateUnitTests(
	ITestOutputHelper outputHelper)
{
	private readonly ITestOutputHelper _outputHelper = outputHelper;

	[Fact]
	public async Task CreateActivityTest()
	{
		// Arrange
		var activity = new MyActivity();

		// Act
		await activity.RunAsync();

		// Assert
		var result = activity.ActivityState;
		result.Should().NotBeNull();
		result.Should().BeOfType<MyActivityState>();
		result.Should().BeEquivalentTo(activity.InitialActivityState);

		var json = DefaultJsonSerializer.Serialize(result);
		_outputHelper.WriteLine(json);
	}
}

public sealed class MyActivity : Activity<MyActivityState>
{
	public MyActivity()
	{
		InitialActivityState = new MyActivityState()
		{
			Id = Guid.NewGuid(),
		};
		ActivityState = InitialActivityState;
	}
}