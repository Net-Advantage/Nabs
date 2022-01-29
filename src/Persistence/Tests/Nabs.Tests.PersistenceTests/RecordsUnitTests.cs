namespace Nabs.Tests.PersistenceTests;

public class RecordsUnitTests
{
    [Fact]
    public void Test1()
    {
        var p1 = new TestUser(Guid.NewGuid(), "dwschreyer", "Darrel");
        var p2 = new TestUser(p1.Id, p1.Username, p1.FirstName);

        var x = p1 == p2;

        var a = p1 as IRelationalEntity<Guid>;
        var aId = a.Id;

        var b = p1 as IRelationalEntity<Guid>;
        var bId = b.GetId();
    }
}