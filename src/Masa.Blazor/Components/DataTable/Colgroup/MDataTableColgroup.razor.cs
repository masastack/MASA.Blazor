namespace Masa.Blazor.Components.DataTable;

public partial class MDataTableColgroup<TItem>
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public List<DataTableHeader<TItem>> Headers { get; set; } = new();

    private bool HasFixedColumn => Headers.Any(u => u.Fixed != DataTableFixed.None);

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var lastFixedLeftHeader = Headers.LastOrDefault(u => u.Fixed == DataTableFixed.Left);
        if (lastFixedLeftHeader != null)
        {
            lastFixedLeftHeader.IsFixedShadowColumn = true;
        }

        var firstFixedRightHeader = Headers.FirstOrDefault(u => u.Fixed == DataTableFixed.Right);
        if (firstFixedRightHeader != null)
        {
            firstFixedRightHeader.IsFixedShadowColumn = true;
        }
    }

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
