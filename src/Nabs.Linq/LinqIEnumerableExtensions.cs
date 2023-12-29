namespace Nabs.Linq;

public static class LinqIEnumerableExtensions
{
	public static bool NotNullAndAny<TSource>(this IEnumerable<TSource>? source)
	{
		return source?.Any() ?? false;
	}
}