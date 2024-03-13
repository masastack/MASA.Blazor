namespace Masa.Blazor.Components.ScrollToTarget;

public class TargetTriggerContext
{
    public bool IsActive { get; internal set; }

    public string? ActiveClass { get; internal set; }

    public Action ScrollToTarget { get; internal set; } = null!;
}
