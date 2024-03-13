namespace Masa.Blazor.Components.DataTable;

public partial class MDataTableRowGroup
{
    [Parameter]
    public RenderFragment? RowHeaderContent { get; set; }

    [Parameter]
    public RenderFragment? RowContentContent { get; set; }

    [Parameter]
    public RenderFragment? ColumnHeaderContent { get; set; }

    [Parameter]
    public bool Value { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string HeaderClass => "m-row-group__header";
}
