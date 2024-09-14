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

    public StyleBuilder AddLength(string name, double value, string unit = "px")
    {
        return Add(value == 0 ? $"{name}: 0" : $"{name}: {value}{unit}".ToString(CultureInfo.InvariantCulture));
    }

    public void Clear()
    {
        _stringBuilder.Clear();
    }

    public IEnumerable<string> GenerateCssStyles()
    {
        yield return _stringBuilder.ToString();
    }

    public string? Build()
    {
        if (_stringBuilder.Length == 0)
        {
            return null;
        }

        return _stringBuilder.ToString();
    }

    public override string? ToString()
    {
        return Build();
    }
}