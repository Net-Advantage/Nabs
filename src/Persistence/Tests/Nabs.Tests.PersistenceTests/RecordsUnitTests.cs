namespace Nabs.Tests.PersistenceTests;

public class RecordsUnitTests
{
    [Fact]
    public void InstantiateTestUser_ToSeeHowRecordsWork()
    {
        var p1 = new TestUser(Guid.NewGuid(), "dwschreyer", "Darrel");
        var p2 = new TestUser(p1.Id, p1.Username, p1.FirstName);

        p1.Should().BeEquivalentTo(p2);
        
        var a = p1 as IRelationalEntity<Guid>;
        a.Id.Should().NotBeEmpty();

        var b = p1 as IRelationalEntity<Guid>;
        b.GetId().Should().NotBeEmpty();
    }
}