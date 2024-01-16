namespace Nabs;

public abstract class ValueObject<T> : IEquatable<T>
	where T : ValueObject<T>
{
	public abstract IEnumerable<object?> GetEqualityComponents();

	public bool Equals(T? other)
	{
		if (other is null)
			return false;

		if (ReferenceEquals(this, other))
			return true;

		return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
	}

	public override int GetHashCode()
	{
		return GetEqualityComponents()
			.Aggregate(1, (current, obj) =>
			{
				unchecked
				{
					return current * 23 + (obj?.GetHashCode() ?? 0);
				}
			});
	}

	public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right)
	{
		if (left is null && right is null)
			return true;

		if (left is null || right is null)
			return false;

		return left.Equals(right);
	}

	public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right)
	{
		return !(left == right);
	}

	public override bool Equals(object? obj)
	{
		if (obj == null || obj.GetType() != GetType())
			return false;

		var other = (T)obj;
		return Equals(other);
	}
}