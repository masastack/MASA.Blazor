namespace Masa.Blazor;

public partial class MPicker : MasaComponentBase
{
    [Parameter] public bool Flat { get; set; }

    [Parameter] public bool FullWidth { get; set; }

    [Parameter] public bool Landscape { get; set; }

    [Parameter] public bool NoTitle { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] [MasaApiParameter(290)] public StringNumber Width { get; set; } = 290;

    [Parameter]
    [MasaApiParameter("fade-transition")]
    public string Transition { get; set; } = "fade-transition";

    [Parameter] public string? HeaderColor { get; set; }
    [Parameter] public RenderFragment? TitleContent { get; set; }

    [Parameter] public RenderFragment? ActionsContent { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    private Block _block = new("m-picker");

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

    public string ComputedTitleColor
    {
        get
        {
            var defaultTitleColor = IsDark ? "" : (HeaderColor ?? Color ?? "primary");
            return HeaderColor ?? Color ?? defaultTitleColor;
        }
    }

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(Flat)
            .And(Landscape)
            .And(FullWidth)
            .AddTheme(IsDark, IndependentTheme)
            .AddElevation(Elevation)
            .AddClass("m-card")
            .GenerateCssClasses();
    }
}