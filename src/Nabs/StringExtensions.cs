namespace Nabs;

public static class StringExtensions
{
    public static string DefaultIfNullOrWhiteSpace(this string? value, string defaultValue)
    {
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    }

    public static string EmptyIfNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }

    public static string? NullIfNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value;
    }
}