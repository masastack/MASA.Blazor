namespace Masa.Blazor;

public class MCounter : ThemeContainer
{
    [Parameter, EditorRequired] public StringNumber Value { get; set; } = null!;

    [Parameter] public StringNumber? Max { get; set; }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-counter";

        if (Max != null && (Value.ToInt32() > Max.ToInt32()))
        {
            yield return "error--text";
        }

        yield return CssClassUtils.GetTheme(ComputedTheme) ?? string.Empty;
    }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.AddContent(0, Max == null ? Value : $"{Value}/{Max}");
    };
}