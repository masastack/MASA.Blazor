namespace Masa.Blazor;

public class MCounter : ThemeContainer
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter, EditorRequired] public StringNumber Value { get; set; } = null!;

    [Parameter] public StringNumber? Max { get; set; }

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

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-counter";

        if (Max != null && (Value.ToInt32() > Max.ToInt32()))
        {
            yield return "error--text";
        }

        yield return CssClassUtils.GetTheme(IsDark, IndependentTheme) ?? string.Empty;
    }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.AddContent(0, Max == null ? Value : $"{Value}/{Max}");
    };
}