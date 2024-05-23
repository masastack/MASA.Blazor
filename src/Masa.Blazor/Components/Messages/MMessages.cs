namespace Masa.Blazor;

public partial class MMessages : MasaComponentBase
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public string? Color { get; set; }

    [Parameter] public List<string> Value { get; set; } = new();

    [Parameter] public RenderFragment<string>? ChildContent { get; set; }

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

    private Block _block = new("m-messages");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.AddTheme(IsDark, IndependentTheme)
            .AddTextColor(Color)
            .GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        yield return CssClassUtils.GetColor(Color) ?? string.Empty;
    }
}