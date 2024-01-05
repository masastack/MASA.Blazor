namespace Masa.Blazor;

public class MDataTableColgroup<TItem> : BDataTableColgroup<TItem>
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    protected override void SetComponentCss()
    {
        base.SetComponentCss();

        CssProvider.Apply("col", css =>
        {
            var header = css.Data as DataTableHeader;
            css.AddIf("divider", () => header?.Divider is true);
        });
    }
}
