namespace Masa.Blazor.Utils;

public class Metadata
{
    public static string Version { get; } = typeof(Metadata).Assembly.GetName().Version?.ToString(3) ?? "0.0.0";
}