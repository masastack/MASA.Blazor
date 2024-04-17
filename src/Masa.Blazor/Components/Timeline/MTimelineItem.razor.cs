namespace Masa.Blazor;

public partial class MTimelineItem : MasaComponentBase, IThemeable
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [CascadingParameter(Name = "Reverse")] public bool Reverse { get; set; }

    [Parameter]
    [MasaApiParameter("primary")]
    public string? Color { get; set; } = "primary";

    [Parameter] public bool FillDot { get; set; }

    [Parameter] public bool HideDot { get; set; }

    [Parameter] public string? Icon { get; set; }

    [Parameter] public string? IconColor { get; set; }

    [Parameter] public bool Large { get; set; }

    [Parameter] public bool Left { get; set; }

    [Parameter] public bool Right { get; set; }

    [Parameter] public bool Small { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public RenderFragment? OppositeContent { get; set; }

    [Parameter] public RenderFragment? IconContent { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

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

    private Block _block = new("m-timeline-item");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(FillDot)
            .And("before", Reverse ? Right : Left)
            .And("after", Reverse ? Left : Right)
            .AddTheme(IsDark, IndependentTheme)
            .GenerateCssClasses();
    }
}