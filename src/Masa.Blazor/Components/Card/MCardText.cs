namespace Masa.Blazor;

public class MCardText : Container
{
    [CascadingParameter(Name = "MasaBlazorCascadingTheme")]
    public string CascadingTheme { get; set; } = null!;

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            new[]
            {
                "m-card__text",
                CssClassUtils.GetTheme(CascadingTheme)
            }
        );
    }
}