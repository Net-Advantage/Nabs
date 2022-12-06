namespace Nabs.ObjectBuilder;

public record PersistentEntityProperty<T>(string Name)
{
	public Type Type => typeof(T);
}