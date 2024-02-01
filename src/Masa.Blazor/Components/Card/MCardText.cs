namespace Masa.Blazor;

public class MCardText : Container
{
    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    protected override string ClassName => "m-card__text";

    protected override void SetComponentCss()
    {
        base.SetComponentCss();
        CssProvider.Merge(css => { css.AddTheme(CascadingIsDark, isIndependent: false); });
    }
}