namespace Masa.Blazor;

public class MAppThemeStylesheet : ComponentBase
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var themCss = ThemeCssBuilder.Build(MasaBlazor.Theme);

        if (string.IsNullOrWhiteSpace(themCss))
        {
            return;
        }

        builder.OpenElement(0, "style");
        builder.AddAttribute(1, "id", "masa-blazor-theme-stylesheet");
        builder.AddAttribute(2, "type", "text/css");
        builder.AddContent(3, (MarkupString)themCss);
        builder.CloseElement();
    }
}