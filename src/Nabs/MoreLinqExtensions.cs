namespace Nabs;

public static class MoreLinqExtensions
{
    public static bool NotNullAndAny<TSource>(this IEnumerable<TSource>? source)
    {
        return source?.Any() ?? false;
    }
}