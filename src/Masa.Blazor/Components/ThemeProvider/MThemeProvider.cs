namespace Masa.Blazor;

public class MThemeProvider : MasaComponentBase
{
    [EditorRequired] [Parameter] public string? Theme { get; set; }

    [Parameter] [MasaApiParameter("div")] public string? Tag { get; set; } = "div";

    [Parameter] public bool WithBackground { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    private string? _prevTheme = string.Empty;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_prevTheme != Theme)
        {
            _prevTheme = Theme;

            if (string.IsNullOrWhiteSpace(Theme))
            {
                Logger.LogWarning("Theme is null or empty, please set a theme.");
            }
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<string>>(0);
        builder.AddAttribute(1, "Value", Theme);
        builder.AddAttribute(2, "Name", "MasaBlazorCascadingTheme");
        builder.AddAttribute(3, "ChildContent", WithBackground ? (RenderFragment?)BuildChildContent : ChildContent);
        builder.CloseComponent();
    }

    private void BuildChildContent(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, Tag ?? "div");
        builder.AddAttribute(1, "id", Id);
        builder.AddAttribute(2, "class", GetClass());
        builder.AddAttribute(3, "style", GetStyle());
        builder.AddElementReferenceCapture(4, r => Ref = r);
        builder.AddContent(5, ChildContent);
        builder.CloseElement();
    }

    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return "m-theme-provider";
        yield return CssClassUtils.GetTheme(Theme);
    }
}