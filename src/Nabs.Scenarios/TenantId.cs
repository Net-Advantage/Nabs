namespace Nabs.Scenarios;

public class TenantId : ValueObject<TenantId>
{
    public Guid Id { get; }

    private TenantId(Guid id)
    {
        Id = id;
    }

    public static TenantId Create(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Tenant id cannot be empty", nameof(id));

        return new(id);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Id;
    }
}
