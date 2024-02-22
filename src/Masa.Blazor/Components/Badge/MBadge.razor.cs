namespace Masa.Blazor;

public partial class MBadge : MasaComponentBase
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public bool Avatar { get; set; }

    [Parameter] public bool Bordered { get; set; }

    [Parameter]
    [MasaApiParameter("primary")]
    public string? Color { get; set; } = "primary";

    [Parameter] public StringNumber? Content { get; set; }

    [Parameter] public bool Dot { get; set; }

    [Parameter] public string? Icon { get; set; }

    [Parameter] public StringNumber? OffsetX { get; set; }

    [Parameter] public StringNumber? OffsetY { get; set; }

    [Parameter] public bool OverLap { get; set; }

    [Parameter] public bool Tile { get; set; }

    [Parameter]
    [MasaApiParameter("scale-rotate-transition")]
    public string Transition { get; set; } = "scale-rotate-transition";

    [Parameter] public bool Value { get; set; } = true;

    [Parameter] public bool Bottom { get; set; }

    [Parameter] public RenderFragment? BadgeContent { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool Inline { get; set; }

    [Parameter] public bool Left { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public bool IsDark => Dark || (!Light && CascadingIsDark);

    private int Offset => OverLap ? (Dot ? 8 : 12) : (Dot ? 2 : 4);

    private const string AUTO = "auto";

    private string CalcPosition(StringNumber? offset)
    {
        var obj = offset != null ? offset.ToUnit() : $"{Offset}px";

        return $"calc(100% - {obj})";
    }

    private string ComputedXOffset => CalcPosition(OffsetX);

    private string ComputedYOffset => CalcPosition(OffsetY);

    private string ComputedBottom => Bottom ? AUTO : ComputedYOffset;

    private string ComputedTop => Bottom ? ComputedYOffset : AUTO;

    private string ComputedLeft => IsRtl ? (Left ? ComputedXOffset : AUTO) : (Left ? AUTO : ComputedXOffset);

    private string ComputedRight => IsRtl ? (Left ? AUTO : ComputedXOffset) : (!Left ? AUTO : ComputedXOffset);

    private bool IsRtl => MasaBlazor.RTL;

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

#if NET8_0_OR_GREATER
        if (MasaBlazor.IsSsr && !IndependentTheme)
        {
            CascadingIsDark = MasaBlazor.Theme.Dark;
        }
#endif
    }

    private Block _block = new("m-badge");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(Avatar)
            .And(Bordered)
            .And(Bottom)
            .And(Dot)
            .And("icon", !string.IsNullOrWhiteSpace(Icon))
            .And(Inline)
            .And(Left)
            .And("overlap", OverLap)
            .And(Tile)
            .AddTheme(IsDark, IndependentTheme)
            .GenerateCssClasses();
    }
}