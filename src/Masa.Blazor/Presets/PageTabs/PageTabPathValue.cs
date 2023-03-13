namespace Masa.Blazor.Presets;

public record PageTabPathValue(string Path, bool Selected)
{
    public bool Equals(string input)
    {
        if (!input.StartsWith("/"))
        {
            input = "/" + input;
        }

        return input.Equals(Path, StringComparison.OrdinalIgnoreCase);
    }
}
