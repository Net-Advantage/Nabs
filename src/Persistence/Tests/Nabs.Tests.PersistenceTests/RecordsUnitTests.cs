namespace Nabs.Tests.PersistenceTests;

public class RecordsUnitTests
{
    private TestUser _user;
    public RecordsUnitTests()
    {
        _user = new TestUser(
            Guid.NewGuid(),
            "theUsername",
            "TheFirstName",
            "TheLastName");
    }

    [Fact]
    public void InstantiateTestUser_ToSeeHowRecordsWork()
    {
        var user = new TestUser(
            _user.Id, 
            _user.Username, 
            _user.FirstName, 
            _user.LastName);

        _user.Should().BeEquivalentTo(user)
            .And.NotBeSameAs(user);
    }

    [Fact]
    public void InstantiateTestUser_EnsureTheSameInstance()
    {
        var user = _user;

        _user.Should().BeEquivalentTo(user)
            .And.BeSameAs(user);
    }

    [Fact]
    public void InstantiateTestUser_EnsureNotTheSameInstance()
    {
        var user = _user with {};

        _user.Should().BeEquivalentTo(user)
            .And.NotBeSameAs(user);
    }

    [Fact]
    public void InstantiateTestUser_EnsureNotEquivalentAndNotSameInstance()
    {
        var user = _user with { Username = "otherUsername"};

        _user.Should().NotBeEquivalentTo(user)
            .And.NotBeSameAs(user);
    }
}