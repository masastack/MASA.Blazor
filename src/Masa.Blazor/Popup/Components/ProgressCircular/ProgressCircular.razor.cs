namespace Masa.Blazor.Popup.Components;

public partial class ProgressCircular : PopupComponentBase
{
    [Parameter] public string? BackgroundColor { get; set; } = "#212121";

    [Parameter] public double BackgroundOpacity { get; set; } = 0.46;

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public string? ScrimClass { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public int ZIndex { get; set; } = 1000;

    [Parameter] public int Size { get; set; } = 72;

    [Parameter] public int Width { get; set; } = 4;

    private string ComputedStyle => $"position: fixed;{Style}";

    protected override string ComponentName => PopupComponents.PROGRESS_CIRCULAR;
}
