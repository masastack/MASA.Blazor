namespace Masa.Blazor.Core;

public class CssBuilder
{
    private readonly StringBuilder _stringBuilder = new();

    public CssBuilder Add(string? value, bool condition)
    {
        return condition ? Add(value) : this;
    }

    public CssBuilder Add(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return this;
        }

        _stringBuilder.Append(' ');
        _stringBuilder.Append(value);
        return this;
    }

    public override string ToString()
    {
        return _stringBuilder.ToString();
    }
}