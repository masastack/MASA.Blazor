namespace Masa.Blazor.ScrollToTarget;

public class TargetContext
{
    public string? ActiveTarget { get; set; }

    public Action<string> ScrollToTarget { get; set; } = default!;
}
