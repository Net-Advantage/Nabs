namespace Nabs.Tests.PersistenceTests;

[Collection(nameof(TestDbContextDataLoaderFixtureCollection))]
public class TestUserUnitTests : TestBase<DataLoaderFixture>
{
	private readonly IRelationalRepository<TestDbContext> _testRepository;
	private readonly ISelectItem<TestUser> _testUserSelect;
	
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
		//Arrange
		var id = new Guid("6d3e6043-0b1a-4bcd-8c24-eba1c23daba5");

		//Act
		var actual = await _testUserSelect
			.WithId(id)
			.ExecuteAsync();

		//Assert
		actual.Should().NotBeNull();
	}
	
	[Fact]
	public async Task CreateAndQueryItem_SpecificTestUser_Success()
	{
		//Arrange
		var id = Guid.NewGuid();
		var newTestUser = new TestUser(id, id.ToString(), id.ToString(), id.ToString());
		var createdItem = await _testRepository
			.UpsertItem<TestUser>()
			.ForItem(newTestUser)
			.ExecuteAsync();

		//Act
		var actual = await _testUserSelect
			.WithPredicate(_ => _.Id == id)
			.ExecuteAsync();

		//Assert
		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(createdItem);
	}

	[Fact]
	public async Task CreateUpdateAndQueryItem_SpecificTestUser_Success()
	{
		//Arrange
		var id = Guid.NewGuid();
		var newTestUser = new TestUser(id, id.ToString(), id.ToString(), id.ToString());
		var updatedItem = await _testRepository
			.UpsertItem<TestUser>()
			.ForItem(newTestUser)
			.ExecuteAsync();

		//Act
		var actual = await _testUserSelect
			.WithPredicate(_ => _.Id == id)
			.ExecuteAsync();

		var itemToUpdate = new TestUser(id, id.ToString(), "fn: updated first name", "ln: updated last name");

		//Assert
		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(updatedItem);
		//updatedItem.Should().NotBeEquivalentTo(newTestUser);
	}

	[Fact]
	public async Task QueryItem_SpecificTestUser_WithProjection_Success()
	{
		//Arrange
		var id = new Guid("6d3e6043-0b1a-4bcd-8c24-eba1c23daba5");

		//Act
		var actual = await _testUserSelect
			.WithPredicate(_ => _.Id == id)
			.ExecuteAsync<TestUserDto>();

		var actual1 = await _testUserSelect
			.WithPredicate(_ => _.Id == id)
			.ExecuteAsync<TestUserDto>();

		//Assert
		actual.Should().NotBeNull();
		actual.FullName.Should().NotBeNullOrWhiteSpace();
	}
}