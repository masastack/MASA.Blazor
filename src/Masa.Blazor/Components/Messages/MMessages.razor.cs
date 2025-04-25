using Element = BemIt.Element;

namespace Masa.Blazor;

public partial class MMessages : ThemeComponentBase
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public string? Color { get; set; }

    [Parameter] public List<string> Value { get; set; } = new();

    [Parameter] public RenderFragment<string>? ChildContent { get; set; }

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

    private static Block _block = new("m-messages");
    private static Element _message = _block.Element("message");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.Name;
        yield return CssClassUtils.GetTheme(ComputedTheme);
        yield return CssClassUtils.GetColor(Color, true);
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        yield return CssClassUtils.GetColor(Color) ?? string.Empty;
    }
}