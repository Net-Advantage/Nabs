namespace Nabs.Persistence;

public interface IRelationalEntity<TKey>
{
    TKey Id { get; init; }

    public TKey GetId()
    {
        return Id;
    }
}