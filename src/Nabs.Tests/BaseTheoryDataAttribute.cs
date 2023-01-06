namespace Nabs.Tests;

[DataDiscoverer("Xunit.Sdk.DataDiscoverer", "xunit.core")]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public abstract class BaseTheoryDataAttribute<T> : Attribute
{
	public abstract IEnumerable<T> GetData(MethodInfo testMethod);

	public virtual string Skip { get; set; }
}