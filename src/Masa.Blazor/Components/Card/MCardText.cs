namespace Masa.Blazor;

public class MCardText : Container
{
    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            new[]
            {
                "m-card__text",
                CssClassUtils.GetTheme(CascadingIsDark, false)
            }
        );
    }
}