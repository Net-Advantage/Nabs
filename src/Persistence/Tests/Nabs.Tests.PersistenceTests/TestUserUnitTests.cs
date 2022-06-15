namespace Nabs.Tests.PersistenceTests;

[Collection(nameof(TestDbContextDataLoaderFixtureCollection))]
public class TestUserUnitTests : TestBase<DataLoaderFixture>
{
	private readonly IRelationalRepository<TestDbContext> _testRepository;
	private readonly ISelectItem<TestUser> _testUserSelect;

	private Guid _id;
	private TestUser _newTestUser;

	public TestUserUnitTests(
		DataLoaderFixture testFixture,
		ITestOutputHelper output)
		: base(testFixture, output)
	{
		TestFixture.Should().NotBeNull();

		_testRepository = TestFixture.ServiceScope.ServiceProvider
			.GetRequiredService<IRelationalRepository<TestDbContext>>();
		_testRepository.Should().NotBeNull();

		_testUserSelect = _testRepository.SelectItem<TestUser>();
		_testUserSelect.Should().NotBeNull();
	}

	public override async Task StartTest()
	{
		//Arrange
		await TestFixture.EnsureDatabaseLoaderAsync();

		_id = Guid.NewGuid();
		_newTestUser = await TestFixture
			.TestDbContextDataLoader!
			.CreateTestUser(_id);
	}

	[Fact]
	public async Task GetItem_FirstTestUser_Success()
	{
		//Act
		var actual = await _testUserSelect
			.ExecuteAsync();

		//Assert
		actual.Should().NotBeNull();
	}


	[Fact]
	public async Task QueryItem_TestUserById_Success()
	{
		//Act
		var actual = await _testUserSelect
			.WithId(_id)
			.ExecuteAsync();

		//Assert
		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(_newTestUser);
	}

	[Fact]
	public async Task QueryItem_SpecificTestUser_Success()
	{
		//Act
		var actual = await _testUserSelect
			.WithPredicate(_ => _.Id == _id)
			.ExecuteAsync();

		//Assert
		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(_newTestUser);
	}

	[Fact]
	public async Task UpdateAndQueryItem_SpecificTestUser_Success()
	{
		//Arrange
		var (id, username, _, _) = _newTestUser;
		var itemToUpdate = new TestUser(id, username, "fn: updated first name", "ln: updated last name");
		var updatedItem = await _testRepository
			.UpsertItem<TestUser>()
			.ForItem(itemToUpdate)
			.ExecuteAsync();

		//Act
		var actual = await _testUserSelect
			.WithPredicate(_ => _.Id == _id)
			.ExecuteAsync();

		//Assert
		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(updatedItem);
		updatedItem.Should().NotBeEquivalentTo(_newTestUser);
	}

	[Fact]
	public async Task QueryItem_SpecificTestUser_WithProjection_Success()
	{
		//Act
		var actual = await _testUserSelect
			.WithPredicate(_ => _.Id == _id)
			.ExecuteAsync<TestUserDto>();

		var actual1 = await _testUserSelect
			.WithPredicate(_ => _.Id == _id)
			.ExecuteAsync<TestUserDto>();

		//Assert
		actual.Should().NotBeNull();
		actual.FullName.Should().NotBeNullOrWhiteSpace();
	}
}