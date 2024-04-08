namespace Masa.Blazor;

public partial class MI18n : BI18n
{
    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider.Apply(cssBuilder => { cssBuilder.Add("m-i18n"); });
    }
}
