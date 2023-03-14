namespace Masa.Blazor.Presets;

public record PageTabPathValue(string AbsolutePath, bool Selected)
{
    public bool IsMatch(string input)
    {
        if (!input.StartsWith("/"))
        {
            input = "/" + input;
        }

        return new Regex(input, RegexOptions.IgnoreCase).IsMatch(AbsolutePath);
    }
}
