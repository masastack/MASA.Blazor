using Element = BemIt.Element;

namespace Masa.Blazor;

public partial class MMessages : ThemeComponentBase
{
    [Parameter] public string? Color { get; set; }

    [Parameter] public List<string> Value { get; set; } = new();

    [Parameter] public RenderFragment<string>? ChildContent { get; set; }

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