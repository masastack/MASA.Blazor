using System.Reflection;

namespace Masa.Blazor.Core.I18n;

internal static class EmbeddedLocales
{
    private static readonly Dictionary<string, string> s_availableResources;
    private static readonly Dictionary<CultureInfo, Dictionary<string, string>> s_localeCaches = new();
    private static readonly Assembly s_resourcesAssembly = typeof(I18n).Assembly;

    static EmbeddedLocales()
    {
        s_availableResources = s_resourcesAssembly
                             .GetManifestResourceNames()
                             .Select(s => Regex.Match(s, @"^.*Locales\.(.+)\.json"))
                             .Where(s => s.Success)
                             .ToDictionary(s => s.Groups[1].Value, s => s.Value);
    }

    public static IReadOnlyDictionary<string, string>? GetSpecifiedLocale(CultureInfo culture)
    {
        if (!s_availableResources.ContainsKey(culture.Name))
            return I18nCache.GetLocale(culture);

        if (s_localeCaches.TryGetValue(culture, out var specifiedLocale))
            return specifiedLocale;

        string fileName = s_availableResources[culture.Name];
        using var fileStream = s_resourcesAssembly.GetManifestResourceStream(fileName);
        if (fileStream == null) return null;
        using var streamReader = new StreamReader(fileStream);
        var content = streamReader.ReadToEnd();

        var locale = I18nReader.Read(content);

        s_localeCaches.TryAdd(culture, locale);

        return locale;
    }

    public static bool ContainsLocale(CultureInfo culture)
    {
        return s_localeCaches.ContainsKey(culture);
    }
}
