namespace Masa.Blazor;

public partial class MPicker
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

    private static Block _block = new("m-picker");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private ModifierBuilder _titleModifierBuilder = _block.Element("title").CreateModifierBuilder();
    private ModifierBuilder _bodyModifierBuilder = _block.Element("body").CreateModifierBuilder();
    private ModifierBuilder _actionsModifierBuilder = _block.Element("actions").CreateModifierBuilder();

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
        yield return _modifierBuilder.Add(Flat, Landscape, FullWidth)
            .AddTheme(ComputedTheme)
            .AddElevation(Elevation)
            .AddClass("m-card")
            .Build();
    }
}