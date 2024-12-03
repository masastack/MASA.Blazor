using System.Diagnostics.CodeAnalysis;

namespace Masa.Blazor.Components.TemplateTable;

public static class IReadOnlyDictionaryExtensions
{
    public static bool TryGetNotNullValue(this IReadOnlyDictionary<string, JsonElement> dict, string key,
        [MaybeNullWhen(false)] out string value)
    {
        var k = dict.Keys.FirstOrDefault(u => u.Equals(key, StringComparison.OrdinalIgnoreCase));
        if (k == null)
        {
            value = default;
            return false;
        }

        value = dict[k].GetString();
        return value is not null;
    }
}