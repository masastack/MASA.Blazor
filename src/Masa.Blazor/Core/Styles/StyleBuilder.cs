namespace Masa.Blazor.Core;

public struct StyleBuilder
{
    private readonly StringBuilder _stringBuilder = new();

    public StyleBuilder()
    {
    }

    public static StyleBuilder Create() => new();

    public StyleBuilder Add(string key, string? value)
    {
        return Add(key, value, false);
    }

    public StyleBuilder Add(string key, string? value, bool important)
    {
        _stringBuilder.Append($"{key}: {value}");
        if (important)
        {
            _stringBuilder.Append(" !important");
        }

        _stringBuilder.Append(';');

        return this;
    }

    public StyleBuilder AddIf(string key, string? value, bool condition, bool important = false)
    {
        return condition ? Add(key, value, important) : this;
    }

    public StyleBuilder Add(string? style)
    {
        if (string.IsNullOrWhiteSpace(style))
        {
            return this;
        }

        style = style.Trim();
        _stringBuilder.Append(style);

        if (!style.EndsWith(';'))
        {
            _stringBuilder.Append(';');
        }

        return this;
    }

    public IEnumerable<string> GenerateCssStyles()
    {
        yield return _stringBuilder.ToString();
    }

    public string Build()
    {
        return _stringBuilder.ToString();
    }

    public override string ToString()
    {
        return Build();
    }
}