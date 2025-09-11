namespace Masa.Blazor.Components.TemplateTable;

public static class IReadOnlyDictionaryExtensions
{
    public static bool TryGetValueWithCaseInsensitiveKey(this IReadOnlyDictionary<string, JsonElement> dict, string key,
        out string? value)
    {

        if (!key.Contains('.'))
        {
            var k = dict.Keys.FirstOrDefault(u => u.Equals(key, StringComparison.OrdinalIgnoreCase));

            if (k == null)
            {
                value = default;
                return false;
            }

            value = dict[k].GetString();
        }
        else
        {
            value = GetNestedValue(dict, key);
        }
        return true;
    }

    private static string? GetNestedValue(IReadOnlyDictionary<string, JsonElement> dict, string key)
    {
        var parts = key.Split('.');
        var index = 0;
        JsonElement? temp = null;
        do
        {
            string? k;
            if (temp.HasValue)
            {
                (k, temp) = GetObjectValue(parts[index], temp);
            }
            else
            {
                k = dict.Keys.FirstOrDefault(u => u.Equals(parts[index], StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrEmpty(k))
                    temp = dict[k];
            }
            if (k == null)
                return null;
            index++;
        }
        while (parts.Length - index > 0);
        return temp?.GetString();
    }

    private static (string?, JsonElement?) GetObjectValue(string key, JsonElement? value)
    {
        if (value == null || value.Value.ValueKind != JsonValueKind.Object)
            return (null, null);

        var item = value.Value.EnumerateObject()
            .FirstOrDefault(i => i.Name.Equals(key, StringComparison.OrdinalIgnoreCase));

        return item.Name != null ? (item.Name, item.Value) : (null, null);
    }
}