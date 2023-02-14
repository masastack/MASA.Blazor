namespace Masa.Docs.Core;

public static class StringExtensions
{
    public static string ToPascal(this string name)
    {
        name = name.Trim('-');
        return string.Join("", name.Split('-').Select(item => char.ToUpper(item[0]) + item.Substring(1)));
    }

    public static string ToKebab(this string name)
    {
        var split = GeneratedRegexes.SingleCamelCase().Split(name).Select(s => s.Trim('-'));
        return string.Join("-", split).ToLowerInvariant();
    }
}
