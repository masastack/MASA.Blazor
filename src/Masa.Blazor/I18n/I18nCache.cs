using System.Collections.Concurrent;
using System.Globalization;

namespace BlazorComponent.I18n;

internal static class I18nCache
{
    private static readonly ConcurrentDictionary<CultureInfo, IReadOnlyDictionary<string, string>> Cache;

    static I18nCache()
    {
        Cache = new ConcurrentDictionary<CultureInfo, IReadOnlyDictionary<string, string>>();
    }

    public static void AddLocale(CultureInfo culture, IReadOnlyDictionary<string, string>? locale,
        bool isBuiltInLocale = false)
    {
        if (locale is null) return;

        Cache.AddOrUpdate(culture, locale, (_, exists) => Merge(exists, locale, isBuiltInLocale));
    }

    public static IReadOnlyDictionary<string, string>? GetLocale(CultureInfo culture)
    {
        return Cache.GetValueOrDefault(culture);
    }

    public static bool ContainsCulture(CultureInfo culture) => Cache.ContainsKey(culture);

    public static bool ContainsCulture(string cultureName) => Cache.Keys.Any(c => c.Name == cultureName);

    public static IEnumerable<CultureInfo> GetCultures() => Cache.Keys;

    private static IReadOnlyDictionary<string, string> Merge(IReadOnlyDictionary<string, string> exists,
        IReadOnlyDictionary<string, string> locale, bool isBuiltInLocale)
    {
        var dict1 = isBuiltInLocale ? locale : exists;
        var dict2 = isBuiltInLocale ? exists : locale;

        var result = dict1.ToDictionary(x => x.Key, x => x.Value);

        foreach (var pair in dict2)
        {
            result[pair.Key] = pair.Value;
        }

        return result;
    }
}