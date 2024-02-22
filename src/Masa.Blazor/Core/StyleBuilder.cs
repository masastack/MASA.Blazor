namespace Masa.Blazor.Core;

public class StyleBuilder
{
    private readonly Dictionary<string, string?> _styles = new();

    public static StyleBuilder Create() => new();

    public StyleBuilder Add(string key, string? value, bool important = false)
    {
        if (_styles.ContainsKey(key))
        {
            _styles.Remove(key);
        }

        _styles.Add(key, important ? $"{value} !important" : value);

        return this;
    }

    public StyleBuilder AddIf(string key, string? value, bool condition, bool important = false)
    {
        return condition ? Add(key, value, important) : this;
    }

    public IEnumerable<string> GenerateCssStyles()
    {
        return _styles
            .Where(u => !string.IsNullOrWhiteSpace(u.Value))
            .Select(x => $"{x.Key}: {x.Value}");
    }

    public string Build()
    {
        var stringBuilder = new StringBuilder();
        foreach (var item in GenerateCssStyles())
        {
            stringBuilder.Append(item);
            stringBuilder.Append("; ");
        }

        return stringBuilder.ToString().TrimEnd();
    }

    public override string ToString()
    {
        return Build();
    }
}