namespace Nabs.Tests.ObjectBuilderUnitTests;

public class BusinessObjectUnitTests
{
	[Fact]
	public void CreateNewObject()
	{
		// Arrange
		var id = Guid.NewGuid();

		// Act
		var o = new ObjectBuilder.PersistentEntityModel(id)
		{
			Name = id.ToString()
		};


		// Assert
		o.Should().NotBeNull();
	}
}