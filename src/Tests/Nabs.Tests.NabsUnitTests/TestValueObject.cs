namespace Nabs.Tests.NabsUnitTests;

public class TestValueObject(string? nullableProperty) 
	: ValueObject<TestValueObject>
{
	public string? NullableProperty { get; } = nullableProperty;

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return NullableProperty;
	}
}
