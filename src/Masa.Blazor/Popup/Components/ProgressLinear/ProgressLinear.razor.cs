namespace Masa.Blazor.Popup.Components;

public partial class ProgressLinear : PopupComponentBase
{
    [Parameter] public string? BackgroundColor { get; set; }

    [Parameter] public double? BackgroundOpacity { get; set; }

    [Parameter] public bool Bottom { get; set; }

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public int Height { get; set; } = 4;

    [Parameter] public bool Stream { get; set; }

    [Parameter] public bool Striped { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public int ZIndex { get; set; } = 1000;

    private string ComputedStyle => $"z-index: {ZIndex};{Style}";

    protected override string ComponentName => PopupComponents.PROGRESS_LINEAR;
}
