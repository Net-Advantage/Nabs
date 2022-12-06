namespace Nabs.ObjectBuilder;

public record PersistentEntityModel(Guid Id)
{
	public required string Name { get; init; }


}
